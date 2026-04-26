@echo off
REM Wrapper portable para `wmsa`. Copiá este archivo a cualquier carpeta que
REM ya esté en tu PATH (ej. C:\Users\yejc2\bin o C:\Windows) y vas a poder
REM invocar `wmsa ...` sin tocar variables de ambiente de Windows.
REM
REM Usa el python del sistema y el paquete instalado con `pip install -e .`.
python -m wmsa.cli %*
