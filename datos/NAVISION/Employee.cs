namespace datos.NAVISION
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Employee")]
    public partial class Employee
    {
        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] timestamp { get; set; }

        [Key]
        [StringLength(20)]
        public string No_ { get; set; }

        [Column("First Name")]
        [Required]
        [StringLength(30)]
        public string First_Name { get; set; }

        [Column("Middle Name")]
        [Required]
        [StringLength(30)]
        public string Middle_Name { get; set; }

        [Column("Last Name")]
        [Required]
        [StringLength(30)]
        public string Last_Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Initials { get; set; }

        [Column("Job Title")]
        [Required]
        [StringLength(30)]
        public string Job_Title { get; set; }

        [Column("Search Name")]
        [Required]
        [StringLength(30)]
        public string Search_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Column("Address 2")]
        [Required]
        [StringLength(50)]
        public string Address_2 { get; set; }

        [Required]
        [StringLength(30)]
        public string City { get; set; }

        [Column("Post Code")]
        [Required]
        [StringLength(20)]
        public string Post_Code { get; set; }

        [Required]
        [StringLength(30)]
        public string County { get; set; }

        [Column("Phone No_")]
        [Required]
        [StringLength(30)]
        public string Phone_No_ { get; set; }

        [Column("Mobile Phone No_")]
        [Required]
        [StringLength(30)]
        public string Mobile_Phone_No_ { get; set; }

        [Column("E-Mail")]
        [Required]
        [StringLength(80)]
        public string E_Mail { get; set; }

        [Column("Alt_ Address Code")]
        [Required]
        [StringLength(10)]
        public string Alt__Address_Code { get; set; }

        [Column("Alt_ Address Start Date")]
        public DateTime Alt__Address_Start_Date { get; set; }

        [Column("Alt_ Address End Date")]
        public DateTime Alt__Address_End_Date { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        [Column("Birth Date")]
        public DateTime Birth_Date { get; set; }

        [Column("Social Security No_")]
        [Required]
        [StringLength(30)]
        public string Social_Security_No_ { get; set; }

        [Column("Union Code")]
        [Required]
        [StringLength(10)]
        public string Union_Code { get; set; }

        [Column("Union Membership No_")]
        [Required]
        [StringLength(30)]
        public string Union_Membership_No_ { get; set; }

        public int Sex { get; set; }

        [Column("Country_Region Code")]
        [Required]
        [StringLength(10)]
        public string Country_Region_Code { get; set; }

        [Column("Manager No_")]
        [Required]
        [StringLength(20)]
        public string Manager_No_ { get; set; }

        [Column("Emplymt_ Contract Code")]
        public int Emplymt__Contract_Code { get; set; }

        [Column("Statistics Group Code")]
        [Required]
        [StringLength(10)]
        public string Statistics_Group_Code { get; set; }

        [Column("Employment Date")]
        public DateTime Employment_Date { get; set; }

        public int Status { get; set; }

        [Column("Inactive Date")]
        public DateTime Inactive_Date { get; set; }

        [Column("Cause of Inactivity Code")]
        [Required]
        [StringLength(10)]
        public string Cause_of_Inactivity_Code { get; set; }

        [Column("Termination Date")]
        public DateTime Termination_Date { get; set; }

        [Column("Grounds for Term_ Code")]
        [Required]
        [StringLength(10)]
        public string Grounds_for_Term__Code { get; set; }

        [Column("Global Dimension 1 Code")]
        [Required]
        [StringLength(20)]
        public string Global_Dimension_1_Code { get; set; }

        [Column("Global Dimension 2 Code")]
        [Required]
        [StringLength(20)]
        public string Global_Dimension_2_Code { get; set; }

        [Column("Resource No_")]
        [Required]
        [StringLength(20)]
        public string Resource_No_ { get; set; }

        [Column("Last Date Modified")]
        public DateTime Last_Date_Modified { get; set; }

        [Required]
        [StringLength(30)]
        public string Extension { get; set; }

        [Required]
        [StringLength(30)]
        public string Pager { get; set; }

        [Column("Fax No_")]
        [Required]
        [StringLength(30)]
        public string Fax_No_ { get; set; }

        [Column("Company E-Mail")]
        [Required]
        [StringLength(80)]
        public string Company_E_Mail { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [Column("Salespers__Purch_ Code")]
        [Required]
        [StringLength(10)]
        public string Salespers__Purch__Code { get; set; }

        [Column("No_ Series")]
        [Required]
        [StringLength(10)]
        public string No__Series { get; set; }

        [Required]
        [StringLength(30)]
        public string RFC { get; set; }

        [Column("Lugar de Nacimiento")]
        [Required]
        [StringLength(30)]
        public string Lugar_de_Nacimiento { get; set; }

        [Required]
        [StringLength(30)]
        public string CURP { get; set; }

        [Column("Fecha Ultimo Aumento")]
        public DateTime Fecha_Ultimo_Aumento { get; set; }

        [Column("CC Direccion")]
        [Required]
        [StringLength(10)]
        public string CC_Direccion { get; set; }

        [Required]
        [StringLength(10)]
        public string Cliente { get; set; }

        [Column("Nombre Cliente")]
        [Required]
        [StringLength(200)]
        public string Nombre_Cliente { get; set; }

        [Column("Resp Area_Gerencia")]
        [Required]
        [StringLength(10)]
        public string Resp_Area_Gerencia { get; set; }

        [Required]
        [StringLength(100)]
        public string Puesto { get; set; }

        [Column("Resp Area")]
        [Required]
        [StringLength(100)]
        public string Resp_Area { get; set; }

        [Column("Usuario Red")]
        [Required]
        [StringLength(30)]
        public string Usuario_Red { get; set; }

        [Column("Resp Gerencia")]
        public int Resp_Gerencia { get; set; }

        [Column("Centro de Costos")]
        [Required]
        [StringLength(10)]
        public string Centro_de_Costos { get; set; }

        [Column("No Celular Oficina")]
        [Required]
        [StringLength(30)]
        public string No_Celular_Oficina { get; set; }

        [Required]
        [StringLength(10)]
        public string PuestoId { get; set; }

        public DateTime FechaInicioContrato { get; set; }

        public DateTime FechaFinContrato { get; set; }

        [Required]
        [StringLength(30)]
        public string FM3Numero { get; set; }

        [Required]
        [StringLength(30)]
        public string OnSite { get; set; }

        public int MotivoBaja { get; set; }

        [Required]
        [StringLength(250)]
        public string ComBaja { get; set; }

        [Required]
        [StringLength(250)]
        public string ComInactividad { get; set; }

        [Required]
        [StringLength(30)]
        public string Banco1 { get; set; }

        [Required]
        [StringLength(30)]
        public string Cuenta1 { get; set; }

        [Required]
        [StringLength(30)]
        public string Clabe1 { get; set; }

        [Required]
        [StringLength(30)]
        public string Banco2 { get; set; }

        [Required]
        [StringLength(30)]
        public string Cuenta2 { get; set; }

        [Required]
        [StringLength(30)]
        public string Clabe2 { get; set; }

        public decimal BMonto { get; set; }

        [Required]
        [StringLength(10)]
        public string NumJefe { get; set; }

        [Column("Tipo Empleado")]
        [Required]
        [StringLength(20)]
        public string Tipo_Empleado { get; set; }

        [Column("Ubicacion Empleado")]
        [Required]
        [StringLength(15)]
        public string Ubicacion_Empleado { get; set; }

        [Column("Funcion Empleado")]
        [Required]
        [StringLength(10)]
        public string Funcion_Empleado { get; set; }

        [Required]
        [StringLength(12)]
        public string Area { get; set; }

        [Column("Responsable Dir")]
        [Required]
        [StringLength(10)]
        public string Responsable_Dir { get; set; }

        [Column("Responsable Ger")]
        [Required]
        [StringLength(10)]
        public string Responsable_Ger { get; set; }

        [Column("Estado Civil")]
        [Required]
        [StringLength(10)]
        public string Estado_Civil { get; set; }

        [Column("Duración Contrato")]
        [Required]
        [StringLength(10)]
        public string Duración_Contrato { get; set; }

        [Required]
        [StringLength(10)]
        public string FM3 { get; set; }

        [Required]
        [StringLength(10)]
        public string CompañiaTel { get; set; }

        [Column("Plan Celular")]
        [Required]
        [StringLength(10)]
        public string Plan_Celular { get; set; }

        [Required]
        [StringLength(10)]
        public string Subordinados { get; set; }

        [Required]
        [StringLength(10)]
        public string Bono { get; set; }

        [Required]
        [StringLength(10)]
        public string Comisiones { get; set; }

        [Required]
        [StringLength(10)]
        public string BPeriodo { get; set; }

        [Required]
        [StringLength(10)]
        public string CPeriodo { get; set; }

        [Required]
        [StringLength(10)]
        public string BTipo { get; set; }

        [Required]
        [StringLength(30)]
        public string UsuarioMod { get; set; }

        [StringLength(10)]
        public string AreaAdministrativa { get; set; }

        [Required]
        [StringLength(30)]
        public string UsuarioRegistro { get; set; }

        public DateTime FechaRegistro { get; set; }

        [Required]
        [StringLength(10)]
        public string Empresa { get; set; }

        [Column("Fecha Alta IMSS")]
        public DateTime Fecha_Alta_IMSS { get; set; }

        [Required]
        [StringLength(30)]
        public string Hijos { get; set; }

        [Required]
        [StringLength(30)]
        public string UsuarioAutoriza { get; set; }

        public DateTime FechaAutoriza { get; set; }

        [Required]
        [StringLength(100)]
        public string MotivoModificacion { get; set; }

        [Required]
        [StringLength(10)]
        public string TipoBaja { get; set; }

        public int ActivoFijo { get; set; }
    }
}