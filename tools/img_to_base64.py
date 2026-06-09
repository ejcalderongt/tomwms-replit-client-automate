#!/usr/bin/env python3
import argparse
import base64
from pathlib import Path


def main() -> None:
    parser = argparse.ArgumentParser(
        description="Convierte una imagen a Base64 y la guarda en un .txt"
    )
    parser.add_argument("image_path", help="Ruta de la imagen de entrada")
    parser.add_argument(
        "-o",
        "--output",
        help="Ruta del archivo de salida .txt (opcional). Si no se define, usa <nombre>_base64.txt en la misma carpeta.",
    )
    args = parser.parse_args()

    image_path = Path(args.image_path)
    if not image_path.exists():
        raise FileNotFoundError(f"No existe la imagen: {image_path}")

    if args.output:
        output_path = Path(args.output)
    else:
        output_path = image_path.with_name(f"{image_path.stem}_base64.txt")

    encoded = base64.b64encode(image_path.read_bytes()).decode("utf-8")
    output_path.write_text(encoded, encoding="utf-8")

    print(f"Listo, base64 guardado en: {output_path}")


if __name__ == "__main__":
    main()
