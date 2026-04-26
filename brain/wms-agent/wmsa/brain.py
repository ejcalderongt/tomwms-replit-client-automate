"""Cliente del TOMWMS Brain (API Replit).

Wrapper fino sobre los endpoints documentados. Expone métodos tipados,
maneja errores HTTP de forma consistente y nunca loguea el token.
"""

from __future__ import annotations

from typing import Any, Optional

import httpx

from .config import Config


class BrainError(Exception):
    """Error consultando el Brain."""


class BrainClient:
    def __init__(self, config: Config, timeout: float = 30.0) -> None:
        if not config.brain.base_url:
            raise BrainError(
                "Brain base_url no configurada. Corré: wmsa config init"
            )
        self._base = config.brain.base_url.rstrip("/")
        self._token = config.brain.token  # puede ser None para GETs
        self._timeout = timeout

    # ---- helpers --------------------------------------------------------

    def _get(self, path: str, params: dict | None = None) -> Any:
        url = f"{self._base}{path}"
        try:
            r = httpx.get(url, params=params, timeout=self._timeout)
        except httpx.HTTPError as e:
            raise BrainError(f"GET {path} falló: {e}") from e
        if r.status_code >= 400:
            raise BrainError(f"GET {path} -> {r.status_code}: {r.text[:200]}")
        return r.json()

    def _post(self, path: str, body: dict) -> Any:
        if not self._token:
            raise BrainError(
                "POST requiere BRAIN_IMPORT_TOKEN. Corré: wmsa config init"
            )
        url = f"{self._base}{path}"
        headers = {"X-Brain-Token": self._token, "Content-Type": "application/json"}
        try:
            r = httpx.post(url, json=body, headers=headers, timeout=self._timeout)
        except httpx.HTTPError as e:
            raise BrainError(f"POST {path} falló: {e}") from e
        if r.status_code >= 400:
            raise BrainError(f"POST {path} -> {r.status_code}: {r.text[:200]}")
        return r.json()

    # ---- endpoints ------------------------------------------------------

    def health(self) -> dict:
        return self._get("/health")

    def search(
        self,
        q: str,
        kind: Optional[str] = None,
        repo: Optional[str] = None,
        limit: int = 20,
    ) -> dict:
        params = {"q": q, "limit": limit}
        if kind:
            params["kind"] = kind
        if repo:
            params["repo"] = repo
        return self._get("/search", params=params)

    def impact(
        self,
        symbol: str,
        kind: Optional[str] = None,
        repo: Optional[str] = None,
        depth: int = 1,
        limit: int = 100,
    ) -> dict:
        params: dict[str, Any] = {"symbol": symbol, "depth": depth, "limit": limit}
        if kind:
            params["kind"] = kind
        if repo:
            params["repo"] = repo
        return self._get("/impact", params=params)

    def dependencies(
        self,
        symbol: str,
        kind: Optional[str] = None,
        repo: Optional[str] = None,
        depth: int = 1,
        limit: int = 100,
    ) -> dict:
        params: dict[str, Any] = {"symbol": symbol, "depth": depth, "limit": limit}
        if kind:
            params["kind"] = kind
        if repo:
            params["repo"] = repo
        return self._get("/dependencies", params=params)

    def writers(
        self,
        symbol: str,
        op: Optional[str] = None,
        kind: Optional[str] = None,
        repo: Optional[str] = None,
        limit: int = 100,
    ) -> dict:
        params: dict[str, Any] = {"symbol": symbol, "limit": limit}
        if op:
            params["op"] = op
        if kind:
            params["kind"] = kind
        if repo:
            params["repo"] = repo
        return self._get("/writers", params=params)

    def reindex(self, kind: str, repos: list[str]) -> dict:
        if kind not in ("vb", "java"):
            raise BrainError("kind debe ser 'vb' o 'java'")
        return self._post(f"/index/{kind}", {"repos": repos})
