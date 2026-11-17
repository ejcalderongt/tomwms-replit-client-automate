import java.util.*;

// #CKFK20261117_REC_CM_RCA. Isolated reproduction; no Android or database writes.
class ReplayCajaMaster {
    static class Det { int id = 2; double cantidad = 7; }
    static List<Det> seleccionar(double... cantidades) {
        Det obj = new Det();
        List<Det> filtrada = new ArrayList<>();
        // Same reference/add sequence as Procesa_Caja_Master, lines 339-345.
        for (double cantidad : cantidades) {
            obj.cantidad = cantidad;
            filtrada.add(obj);
        }
        return filtrada;
    }
    public static void main(String[] args) {
        for (double[] valores : new double[][]{{4, 3}, {3, 4}, {7}}) {
            List<Det> lista = seleccionar(valores);
            double total = lista.stream().mapToDouble(d -> d.cantidad).sum();
            double esperado = Arrays.stream(valores).sum();
            System.out.println("entrada=" + Arrays.toString(valores)
                + " salida=" + lista.stream().map(d -> d.cantidad).toList()
                + " total=" + total + " esperado=" + esperado
                + " mismaReferencia=" + (lista.get(0) == lista.get(lista.size()-1)));
            if (valores.length == 2 && valores[0] == 4 && total != 6) throw new AssertionError();
            if (valores.length == 2 && valores[0] == 3 && total != 8) throw new AssertionError();
            if (valores.length == 1 && total != 7) throw new AssertionError();
        }
    }
}
