using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ES_Eventos_web_app.Models
{
    public class PaqueteHolder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PaqueteHolder()
        {
            this.RecursoXPaquete = new HashSet<RecursoXPaquete>();
            this.Reservacion = new HashSet<Reservacion>();
        }

        public int id { get; set; }
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        [DisplayName("Precio")]
        public decimal precio { get; set; }
        [DisplayName("Descripción")]
        public string descripcion { get; set; }

        public int idTipoRecurso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecursoXPaquete> RecursoXPaquete { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservacion> Reservacion { get; set; }
    }
}
