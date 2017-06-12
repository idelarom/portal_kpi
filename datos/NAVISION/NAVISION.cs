namespace datos.NAVISION
{
    using System.Data.Entity;

    public partial class NAVISION : DbContext
    {
        public NAVISION()
            : base("name=NAVISION")
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.timestamp)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .Property(e => e.No_)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Middle_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Initials)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Job_Title)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Search_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Address_2)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Post_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.County)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Phone_No_)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Mobile_Phone_No_)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.E_Mail)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Alt__Address_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Social_Security_No_)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Union_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Union_Membership_No_)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Country_Region_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Manager_No_)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Statistics_Group_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Cause_of_Inactivity_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Grounds_for_Term__Code)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Global_Dimension_1_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Global_Dimension_2_Code)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Resource_No_)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Extension)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Pager)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Fax_No_)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Company_E_Mail)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Salespers__Purch__Code)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.No__Series)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.RFC)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Lugar_de_Nacimiento)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.CURP)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.CC_Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Cliente)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Nombre_Cliente)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Resp_Area_Gerencia)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Puesto)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Resp_Area)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Usuario_Red)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Centro_de_Costos)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.No_Celular_Oficina)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PuestoId)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FM3Numero)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.OnSite)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.ComBaja)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.ComInactividad)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Banco1)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Cuenta1)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Clabe1)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Banco2)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Cuenta2)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Clabe2)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.BMonto)
                .HasPrecision(38, 20);

            modelBuilder.Entity<Employee>()
                .Property(e => e.NumJefe)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Tipo_Empleado)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Ubicacion_Empleado)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Funcion_Empleado)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Area)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Responsable_Dir)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Responsable_Ger)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Estado_Civil)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Duración_Contrato)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FM3)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.CompañiaTel)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Plan_Celular)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Subordinados)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Bono)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Comisiones)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.BPeriodo)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.CPeriodo)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.BTipo)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.UsuarioMod)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.AreaAdministrativa)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.UsuarioRegistro)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Empresa)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Hijos)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.UsuarioAutoriza)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.MotivoModificacion)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.TipoBaja)
                .IsUnicode(false);
        }
    }
}