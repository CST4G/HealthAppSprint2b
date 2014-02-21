using System.Data.Entity;

namespace healthApp.Models {
    public class PDFForm {
        //one variable for each feild in the table
        public int ID { get; set; }
        //Title of the document for displaying on the webapp eg. Patient Discharge Form 
        public string Title { get; set; }
        //fileName of the document for storing eg. patientDischargeForm  note does not contain .pdf
        public string fileName { get; set; }
    }

    public class FormDBContext : DbContext {
        public FormDBContext()
            : base( "DefaultConnection" ) {
        }
        public DbSet<PDFForm> PDFForms { get; set; }
    }
}