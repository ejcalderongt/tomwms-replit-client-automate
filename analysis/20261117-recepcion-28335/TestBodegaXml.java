import com.dts.classes.Mantenimientos.Bodega.clsBeBodega;
import org.simpleframework.xml.core.Persister;

// #CKFK20261117_BODEGA_COMPATIBILIDAD_WS: actual HH entity and XML parser.
class TestBodegaXml {
    public static void main(String[] args) throws Exception {
        Persister parser = new Persister();
        for (boolean value : new boolean[]{true, false}) {
            clsBeBodega b = parser.read(clsBeBodega.class,
                "<clsBeBodega><IdBodega>1</IdBodega><permitir_abrir_recepcion_enviada>"
                + value + "</permitir_abrir_recepcion_enviada></clsBeBodega>");
            if (b.permitir_abrir_recepcion_enviada != value) throw new AssertionError();
        }
        if (parser.read(clsBeBodega.class, "<clsBeBodega/>").permitir_abrir_recepcion_enviada)
            throw new AssertionError("Missing optional field must default to false");
        System.out.println("PASS: actual clsBeBodega parses true, false and absent optional field.");
    }
}
