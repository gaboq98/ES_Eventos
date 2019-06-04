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

        public int pId { get; set; }
        [DisplayName("Nombre")]
        public string pNombre { get; set; }
        [DisplayName("Precio")]
        public decimal pPrecio { get; set; }
        [DisplayName("Descripción")]
        public string pDescripcion { get; set; }
        public int rId { get; set; }
        public string rNombre { get; set; }
        public string rCorreo { get; set; }
        public string rTelefono { get; set; }
        public string rUbicacion { get; set; }
        public string rEstado { get; set; }
        public int rTipo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecursoXPaquete> RecursoXPaquete { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservacion> Reservacion { get; set; }
    }
}
