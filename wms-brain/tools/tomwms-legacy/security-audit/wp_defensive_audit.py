#!/usr/bin/env python3
import argparse
import json
import socket
import ssl
from datetime import datetime, timezone
from urllib.parse import urlparse

import requests


SECURITY_HEADERS = [
    "Strict-Transport-Security",
    "Content-Security-Policy",
    "X-Frame-Options",
    "X-Content-Type-Options",
    "Referrer-Policy",
    "Permissions-Policy",
]

COMMON_WP_PATHS = [
    "/wp-json/",
    "/xmlrpc.php",
    "/readme.html",
    "/robots.txt",
    "/sitemap_index.xml",
]


def normalize_url(raw_url: str) -> str:
    parsed = urlparse(raw_url)
    if not parsed.scheme:
        raw_url = f"https://{raw_url}"
        parsed = urlparse(raw_url)
    return f"{parsed.scheme}://{parsed.netloc}"


def fetch(session: requests.Session, url: str, timeout: int = 10) -> dict:
    out = {"url": url, "ok": False, "status_code": None, "error": None, "headers": {}, "body_sample": ""}
    try:
        resp = session.get(url, timeout=timeout, allow_redirects=True)
        out["ok"] = True
        out["status_code"] = resp.status_code
        out["headers"] = dict(resp.headers)
        out["body_sample"] = resp.text[:500]
    except Exception as exc:
        out["error"] = str(exc)
    return out


def check_tls(host: str, port: int = 443, timeout: int = 8) -> dict:
    out = {
        "host": host,
        "port": port,
        "ok": False,
        "error": None,
        "tls_version": None,
        "cipher": None,
        "cert_subject": None,
        "cert_issuer": None,
        "cert_not_before": None,
        "cert_not_after": None,
    }
    try:
        context = ssl.create_default_context()
        with socket.create_connection((host, port), timeout=timeout) as sock:
            with context.wrap_socket(sock, server_hostname=host) as ssock:
                cert = ssock.getpeercert()
                out["ok"] = True
                out["tls_version"] = ssock.version()
                out["cipher"] = ssock.cipher()
                out["cert_subject"] = cert.get("subject")
                out["cert_issuer"] = cert.get("issuer")
                out["cert_not_before"] = cert.get("notBefore")
                out["cert_not_after"] = cert.get("notAfter")
    except Exception as exc:
        out["error"] = str(exc)
    return out


def evaluate_security_headers(headers: dict) -> dict:
    present = {}
    for h in SECURITY_HEADERS:
        present[h] = headers.get(h)
    return present


def evaluate_cookies(headers: dict) -> list:
    cookies = headers.get("Set-Cookie")
    if not cookies:
        return []
    if isinstance(cookies, str):
        raw = [c.strip() for c in cookies.split(",")]
    else:
        raw = [str(cookies)]

    findings = []
    for cookie in raw:
        findings.append(
            {
                "cookie": cookie[:250],
                "has_secure": "secure" in cookie.lower(),
                "has_httponly": "httponly" in cookie.lower(),
                "has_samesite": "samesite" in cookie.lower(),
            }
        )
    return findings


def wp_fingerprint(base_url: str, session: requests.Session) -> dict:
    result = {}
    for path in COMMON_WP_PATHS:
        data = fetch(session, f"{base_url}{path}")
        result[path] = {
            "ok": data["ok"],
            "status_code": data["status_code"],
            "server": data["headers"].get("Server") if data["headers"] else None,
            "x_powered_by": data["headers"].get("X-Powered-By") if data["headers"] else None,
            "body_sample": data["body_sample"],
            "error": data["error"],
        }
    return result


def summarize_risk(main_headers: dict, cookies: list, wp_paths: dict) -> list:
    risks = []
    if not main_headers.get("Strict-Transport-Security"):
        risks.append("Falta HSTS: riesgo de downgrade/SSL stripping en primeras conexiones.")
    if not main_headers.get("Content-Security-Policy"):
        risks.append("Falta CSP: mayor exposición a XSS en contenido dinámico.")
    if not main_headers.get("X-Frame-Options"):
        risks.append("Falta X-Frame-Options: posible clickjacking.")
    if not main_headers.get("X-Content-Type-Options"):
        risks.append("Falta X-Content-Type-Options: riesgo de MIME sniffing.")

    for c in cookies:
        if not c["has_secure"]:
            risks.append("Cookie sin Secure detectada.")
        if not c["has_httponly"]:
            risks.append("Cookie sin HttpOnly detectada.")
        if not c["has_samesite"]:
            risks.append("Cookie sin SameSite detectada.")

    xmlrpc_status = wp_paths.get("/xmlrpc.php", {}).get("status_code")
    if xmlrpc_status in (200, 405):
        risks.append("xmlrpc.php expuesto: revisar necesidad operativa y controles anti abuso.")

    readme_status = wp_paths.get("/readme.html", {}).get("status_code")
    if readme_status == 200:
        risks.append("readme.html público: posible fuga de versión/metadatos.")

    return risks


def run(url: str, output_prefix: str, timeout: int = 10) -> dict:
    base_url = normalize_url(url)
    parsed = urlparse(base_url)
    host = parsed.netloc

    session = requests.Session()
    session.headers.update(
        {
            "User-Agent": "WMS-Defensive-Security-Audit/1.0 (+authorized testing only)",
            "Accept": "*/*",
        }
    )

    root = fetch(session, base_url, timeout=timeout)
    login = fetch(session, f"{base_url}/wp-login.php", timeout=timeout)
    tls = check_tls(host)
    wp_paths = wp_fingerprint(base_url, session)

    header_eval = evaluate_security_headers(root["headers"] if root["headers"] else {})
    cookie_eval = evaluate_cookies(login["headers"] if login["headers"] else {})
    risks = summarize_risk(header_eval, cookie_eval, wp_paths)

    report = {
        "timestamp_utc": datetime.now(timezone.utc).isoformat(),
        "target": base_url,
        "root_status": root["status_code"],
        "login_status": login["status_code"],
        "server_banner": (root["headers"] or {}).get("Server"),
        "x_powered_by": (root["headers"] or {}).get("X-Powered-By"),
        "tls": tls,
        "security_headers": header_eval,
        "login_cookies": cookie_eval,
        "wordpress_paths": wp_paths,
        "risk_summary": risks,
        "scope_notice": "No se realizaron ataques activos, bypass de auth ni fuerza bruta.",
    }

    json_path = f"{output_prefix}.json"
    txt_path = f"{output_prefix}.txt"
    with open(json_path, "w", encoding="utf-8") as f:
        json.dump(report, f, indent=2, ensure_ascii=False)

    with open(txt_path, "w", encoding="utf-8") as f:
        f.write(f"Target: {report['target']}\n")
        f.write(f"Timestamp UTC: {report['timestamp_utc']}\n")
        f.write(f"Root status: {report['root_status']}\n")
        f.write(f"WP login status: {report['login_status']}\n")
        f.write(f"Server: {report['server_banner']}\n")
        f.write(f"TLS version: {report['tls'].get('tls_version')}\n")
        f.write("\nSecurity headers:\n")
        for k, v in report["security_headers"].items():
            f.write(f"- {k}: {'OK' if v else 'MISSING'}\n")
        f.write("\nRisk summary:\n")
        if report["risk_summary"]:
            for r in report["risk_summary"]:
                f.write(f"- {r}\n")
        else:
            f.write("- No high-confidence findings in passive checks.\n")
        f.write("\nNotice: ")
        f.write(report["scope_notice"])
        f.write("\n")

    report["artifacts"] = {"json": json_path, "txt": txt_path}
    return report


def main():
    parser = argparse.ArgumentParser(description="WordPress defensive fingerprint and security audit helper")
    parser.add_argument("--url", required=True, help="Target URL or host")
    parser.add_argument(
        "--output-prefix",
        default="wp_defensive_audit_report",
        help="Output file prefix (without extension)",
    )
    parser.add_argument("--timeout", type=int, default=10, help="Request timeout in seconds")
    args = parser.parse_args()

    report = run(args.url, args.output_prefix, timeout=args.timeout)
    print(json.dumps({"status": "ok", "artifacts": report["artifacts"], "target": report["target"]}, indent=2))


if __name__ == "__main__":
    main()
