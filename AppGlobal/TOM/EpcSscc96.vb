Imports System.Numerics
Imports System.Text

Public Module EpcSscc96

    ''' <summary>
    ''' Codifica un SSCC (GS1) de 18 dígitos a EPC Tag URI binario SSCC-96 (96 bits) y lo devuelve en HEX (24 chars).
    '''
    ''' Estructura SSCC-96 (EPCglobal / GS1):
    '''   - Header:    8 bits  (0x31 => SSCC-96)
    '''   - Filter:    3 bits  (0..7)
    '''   - Partition: 3 bits  (depende de la longitud del GS1 Company Prefix)
    '''   - Company Prefix: variable (aquí fijo: 7 dígitos => 24 bits)
    '''   - Serial Reference: variable (aquí 10 dígitos => 58 bits)
    '''
    ''' Para Company Prefix de 7 dígitos:
    '''   Partition = 5, CompanyPrefixBits = 24, SerialRefBits = 58  (24 + 58 = 82 bits de payload)
    '''
    ''' Nota importante:
    '''   El campo "Serial Reference" en SSCC-96 incluye el Extension Digit (primer dígito del SSCC)
    '''   concatenado con el serial reference GS1 (sin el check digit).
    ''' </summary>
    Public Function EncodeSsccToEpc96_WithPrefix7406171(sscc As String,
                                                        Optional filterValue As Integer = 0,
                                                        Optional validateCheckDigit As Boolean = True) As String

        '========================
        ' 1) Validación de entrada
        '========================
        If sscc Is Nothing Then Throw New ArgumentNullException(NameOf(sscc))
        sscc = sscc.Trim()

        If sscc.Length <> 18 OrElse Not sscc.All(AddressOf Char.IsDigit) Then
            Throw New ArgumentException("El SSCC debe ser numérico y tener exactamente 18 dígitos.", NameOf(sscc))
        End If

        If filterValue < 0 OrElse filterValue > 7 Then
            Throw New ArgumentOutOfRangeException(NameOf(filterValue), "Filter debe estar entre 0 y 7 (3 bits).")
        End If

        '========================
        ' 2) Parámetros GS1 fijos para este patrón
        '========================
        Const companyPrefix As String = "7406171"    ' GS1 Company Prefix (7 dígitos)
        Const companyPrefixDigits As Integer = 7
        Const partitionValue As Integer = 5         ' Tabla SSCC-96: 7 dígitos => partition = 5
        Const companyPrefixBits As Integer = 24      ' Tabla SSCC-96: 7 dígitos => 24 bits
        Const serialRefBits As Integer = 58          ' Complemento a 82 bits: 82 - 24 = 58
        Const headerBits As String = "00110001"      ' 0x31 => SSCC-96

        '========================
        ' 3) Validar el Company Prefix embebido en el SSCC
        '   SSCC (18): [Extension(1)] [CompanyPrefix(7)] [SerialRef(9)] [CheckDigit(1)]
        '========================
        Dim extDigit As String = sscc.Substring(0, 1)
        Dim cpFromSscc As String = sscc.Substring(1, companyPrefixDigits)

        If cpFromSscc <> companyPrefix Then
            Throw New ArgumentException($"El SSCC no corresponde al Company Prefix esperado ({companyPrefix}). Detectado: {cpFromSscc}.",
                                        NameOf(sscc))
        End If

        '========================
        ' 4) Validación opcional del check digit (GS1 Mod-10)
        '   Se calcula sobre los primeros 17 dígitos (sin el dígito verificador).
        '========================
        If validateCheckDigit Then
            Dim expectedCheck As Integer = ComputeGs1Mod10CheckDigit(sscc.Substring(0, 17))
            Dim actualCheck As Integer = CInt(sscc.Substring(17, 1))
            If expectedCheck <> actualCheck Then
                Throw New ArgumentException($"Check digit inválido. Esperado: {expectedCheck}, recibido: {actualCheck}.", NameOf(sscc))
            End If
        End If

        '========================
        ' 5) Construcción de campos EPC
        '========================

        ' Filter (3 bits)
        Dim filterBits As String = Convert.ToString(filterValue, 2).PadLeft(3, "0"c)

        ' Partition (3 bits)
        Dim partitionBits As String = Convert.ToString(partitionValue, 2).PadLeft(3, "0"c)

        ' Company Prefix a entero y luego binario con longitud fija (24 bits)
        Dim cpValue As BigInteger = BigInteger.Parse(companyPrefix)
        Dim cpBin As String = BigIntegerToBinary(cpValue).PadLeft(companyPrefixBits, "0"c)

        ' Serial Reference en SSCC-96:
        '   = Extension Digit + SerialReferenceGS1 (sin check digit)
        ' Para CP=7: SerialReferenceGS1 tiene 9 dígitos.
        Dim serialRefGs1 As String = sscc.Substring(1 + companyPrefixDigits, 16 - companyPrefixDigits) ' 16-7=9
        Dim serialRefField As String = extDigit & serialRefGs1 ' total 10 dígitos

        Dim srValue As BigInteger = BigInteger.Parse(serialRefField)
        Dim srBin As String = BigIntegerToBinary(srValue).PadLeft(serialRefBits, "0"c)

        ' Ensamblaje final: 8 + 3 + 3 + 24 + 58 = 96 bits
        Dim fullBin As String = headerBits & filterBits & partitionBits & cpBin & srBin

        If fullBin.Length <> 96 Then
            Throw New InvalidOperationException($"EPC construido con longitud inválida: {fullBin.Length} bits (se esperaban 96).")
        End If

        '========================
        ' 6) Serialización a HEX (24 chars)
        '========================
        Dim hex As New StringBuilder(24)
        For i As Integer = 0 To 95 Step 4
            Dim nibble As String = fullBin.Substring(i, 4)
            hex.Append(Convert.ToInt32(nibble, 2).ToString("X1"))
        Next

        Return hex.ToString().ToUpperInvariant()
    End Function

    ''' <summary>
    ''' Calcula el dígito verificador GS1 (Mod-10) para una cadena numérica SIN el check digit.
    ''' Regla: desde la derecha, pesos alternos 3 y 1 (empezando con 3 en el dígito más a la derecha).
    ''' </summary>
    Private Function ComputeGs1Mod10CheckDigit(withoutCheckDigit As String) As Integer
        Dim sum As Integer = 0
        Dim weight As Integer = 3

        For i As Integer = withoutCheckDigit.Length - 1 To 0 Step -1
            Dim d As Integer = AscW(withoutCheckDigit(i)) - AscW("0"c)
            sum += d * weight
            weight = If(weight = 3, 1, 3)
        Next

        Dim modv As Integer = sum Mod 10
        Return If(modv = 0, 0, 10 - modv)
    End Function

    ''' <summary>
    ''' Convierte un BigInteger no-negativo a binario sin prefijos ("1010...").
    ''' Implementación manual para evitar dependencias de overloads de Convert con tipos grandes.
    ''' </summary>
    Private Function BigIntegerToBinary(value As BigInteger) As String
        If value = 0 Then Return "0"

        Dim sb As New StringBuilder()
        Dim v As BigInteger = value

        While v > 0
            sb.Insert(0, If((v And 1) = 1, "1"c, "0"c))
            v >>= 1
        End While

        Return sb.ToString()
    End Function

End Module