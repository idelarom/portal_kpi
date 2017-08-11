using negocio.Componentes;
using System;
using System.Data;
using System.DirectoryServices;
using System.IO;
using System.Windows.Forms;

namespace presentacion_wf
{
    public partial class Form1 : Form
    {
        private string[] allfiles;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "Custom Description";

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string sSelectedPath = fbd.SelectedPath;
                    lblpath.Text = sSelectedPath;
                    allfiles = System.IO.Directory.GetFiles(sSelectedPath, "*.*", System.IO.SearchOption.AllDirectories);
                    foreach (string value in allfiles)
                    {
                        FileInfo info = new FileInfo(value);
                        ListViewItem item = new ListViewItem();
                        item.Text = info.Name;
                        ltvPictures.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsubir_Click(object sender, EventArgs e)
        {
            try
            {
                bool correct = true;
                DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + "MIGESA.NET", "IDELAROM", "IH1706RM");
                EmpleadosCOM empleados = new EmpleadosCOM();
                DataSet ds = empleados.sp_listado_empleados(8100, true, false);
                DataTable dt_empleados = ds.Tables[0];
                foreach (DataRow row in dt_empleados.Rows)
                {
                    string usuario = row["usuario"].ToString().Trim().ToUpper();
                    string numempleado = row["num_empleado"].ToString().Trim().ToUpper();
                    string pathimage = ImageUser(numempleado.ToUpper());
                    if (pathimage != "")
                    {
                        string password_user = PasswordUser(usuario);
                        //Create a searcher on your DirectoryEntry
                        DirectorySearcher adSearch = new DirectorySearcher(directoryEntry);
                        adSearch.SearchScope = SearchScope.Subtree;    //Look into all subtree during the search
                        adSearch.Filter = "(&(ObjectClass=user)(sAMAccountName=" + usuario + "))";    //Filter information, here i'm looking at a user with given username
                        if (adSearch != null)
                        {
                            byte[] imgdata = System.IO.File.ReadAllBytes(pathimage);
                            DirectoryEntry resultEntry = new DirectoryEntry();
                            SearchResult sResult = adSearch.FindOne();       //username is unique, so I want to find only one
                            resultEntry = sResult.GetDirectoryEntry();
                            correct = setProperty(resultEntry, "thumbnailPhoto", imgdata);
                        }
                    }
                }

                if (correct) { MessageBox.Show("Imagenes subidas correctamente", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Boolean setProperty(DirectoryEntry thisEntry, string toProperty, byte[] valueToSet)

        {
            if (thisEntry.Properties.Contains(toProperty))

            {
                // if something has been set in this property in the past will go here

                try

                {
                    thisEntry.Properties[toProperty][0] = valueToSet;

                    thisEntry.CommitChanges();
                }
                catch (Exception ex)

                {
                    MessageBox.Show(ex.Message, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else

            {
                // if nothing has ever been set in this property in the past will go here

                try

                {
                    thisEntry.Properties[toProperty].Add(valueToSet);

                    thisEntry.CommitChanges();
                }
                catch (Exception ex)

                {
                    MessageBox.Show(ex.Message, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        public string ImageUser(string user)
        {
            try
            {
                string folder = "";
                foreach (string value in allfiles)
                {
                    FileInfo info = new FileInfo(value);
                    string name = info.Name.ToUpper().ToString().Split('.')[0];
                    name = name.Split('-')[0].Trim();
                    if (user.ToUpper() == name)
                    {
                        folder = info.FullName;
                        break;
                    }
                }
                return folder;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string PasswordUser(string username)
        {
            try
            {
                string PASS = "";
                DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://" + "MIGESA.NET", "IDELAROM", "IH1706RM");
                DirectorySearcher adSearch = new DirectorySearcher(directoryEntry);
                adSearch.SearchScope = SearchScope.Subtree;    //Look into all subtree during the search
                adSearch.Filter = "(&(ObjectClass=user)(sAMAccountName=" + username + "))";    //Filter information, here i'm looking at a user with given username
                if (adSearch != null)
                {
                    SearchResult sResult = adSearch.FindOne();       //username is unique, so I want to find only one
                    string imagen = "";
                    if (sResult != null)
                    {
                        if (sResult.Properties["userPassword"].Count > 0)
                        {
                            PASS = sResult.Properties["userPassword"][0] == null ? "" : sResult.Properties["userPassword"][0].ToString();
                        }
                    }
                }
                return PASS;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}