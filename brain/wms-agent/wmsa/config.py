"""Manejo de configuración y credenciales.

Estrategia:
- Config NO sensible (URL del Brain, host SQL, nombre de DB, user) en
  ~/.wmsa/config.json — texto plano, editable.
- Config SENSIBLE (token Brain, password DB) en el keyring del sistema
  operativo (Credential Manager en Windows). Nunca en disco plano.
- Variables de entorno tienen prioridad sobre todo (útil para CI o overrides).

Las credenciales que no estén configuradas se piden interactivamente la
primera vez que un comando las necesita. La BD puede cambiar de cliente
en cliente, por eso `db.name` es editable sin re-pedir password.
"""

from __future__ import annotations

import json
import os
from dataclasses import dataclass, field, asdict
from pathlib import Path
from typing import Optional

import keyring

CONFIG_DIR = Path.home() / ".wmsa"
CONFIG_FILE = CONFIG_DIR / "config.json"
KEYRING_SERVICE = "wms-agent"


@dataclass
class BrainConfig:
    base_url: str = ""

    @property
    def token(self) -> Optional[str]:
        return os.environ.get("BRAIN_IMPORT_TOKEN") or keyring.get_password(
            KEYRING_SERVICE, "brain.token"
        )


@dataclass
class DbConfig:
    host: str = ""
    name: str = ""
    user: str = ""

    @property
    def password(self) -> Optional[str]:
        # Env override > keyring (la password puede ser distinta por cliente,
        # se guarda con clave "db.password.<host>.<name>" para soportar varias).
        env = os.environ.get("WMS_KILLIOS_DB_PASSWORD")
        if env:
            return env
        if not (self.host and self.name):
            return None
        return keyring.get_password(
            KEYRING_SERVICE, f"db.password.{self.host}.{self.name}"
        )


@dataclass
class Config:
    brain: BrainConfig = field(default_factory=BrainConfig)
    db: DbConfig = field(default_factory=DbConfig)

    @classmethod
    def load(cls) -> "Config":
        # Bug fix (Erik 2026-04-21): cargar SIEMPRE las env vars, exista o no
        # ~/.wmsa/config.json. Si no hay archivo, partimos de un dict vacío
        # pero igual aplicamos overrides desde el ambiente.
        data: dict = {}
        if CONFIG_FILE.exists():
            try:
                data = json.loads(CONFIG_FILE.read_text(encoding="utf-8"))
            except (OSError, json.JSONDecodeError):
                data = {}
        cfg = cls()
        cfg.brain.base_url = (
            os.environ.get("BRAIN_BASE_URL")
            or data.get("brain", {}).get("base_url", "")
        )
        cfg.db.host = (
            os.environ.get("WMS_KILLIOS_DB_HOST")
            or data.get("db", {}).get("host", "")
        )
        cfg.db.name = (
            os.environ.get("WMS_KILLIOS_DB_NAME")
            or data.get("db", {}).get("name", "")
        )
        cfg.db.user = (
            os.environ.get("WMS_KILLIOS_DB_USER")
            or data.get("db", {}).get("user", "")
        )
        return cfg

    def save(self) -> None:
        CONFIG_DIR.mkdir(parents=True, exist_ok=True)
        # Solo persistimos lo no sensible.
        data = {
            "brain": {"base_url": self.brain.base_url},
            "db": {
                "host": self.db.host,
                "name": self.db.name,
                "user": self.db.user,
            },
        }
        CONFIG_FILE.write_text(json.dumps(data, indent=2), encoding="utf-8")

    def store_brain_token(self, token: str) -> None:
        keyring.set_password(KEYRING_SERVICE, "brain.token", token)

    def store_db_password(self, password: str) -> None:
        if not (self.db.host and self.db.name):
            raise ValueError("DB host y name deben configurarse antes del password")
        keyring.set_password(
            KEYRING_SERVICE,
            f"db.password.{self.db.host}.{self.db.name}",
            password,
        )

    def clear_secrets(self) -> None:
        try:
            keyring.delete_password(KEYRING_SERVICE, "brain.token")
        except keyring.errors.PasswordDeleteError:
            pass
        if self.db.host and self.db.name:
            try:
                keyring.delete_password(
                    KEYRING_SERVICE,
                    f"db.password.{self.db.host}.{self.db.name}",
                )
            except keyring.errors.PasswordDeleteError:
                pass

    def to_safe_dict(self) -> dict:
        """Para `wmsa config show` — nunca incluye secrets."""
        return {
            "brain": {
                "base_url": self.brain.base_url,
                "token_set": bool(self.brain.token),
            },
            "db": {
                "host": self.db.host,
                "name": self.db.name,
                "user": self.db.user,
                "password_set": bool(self.db.password),
            },
            "config_file": str(CONFIG_FILE),
        }
