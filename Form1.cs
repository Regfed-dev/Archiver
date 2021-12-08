using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace kljmhnbgfvcd
{
    public partial class Archiver : Form
    {
        public FileStream f = null;
        public FileStream r = null;
        public Archiver()
        { 
        InitializeComponent();
        }

        private void Archive_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выберите файл для архивации.");
            
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            
            string file_path = openFileDialog1.FileName;
           
            MessageBox.Show("Выберите куда сохранить архив.");
            
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            
            string Format = file_path.Substring(file_path.LastIndexOf('.') + 1);
            string archive_name = openFileDialog1.SafeFileName.Replace(Format, "de");
            string archive_path = folderBrowserDialog1.SelectedPath;
            
            if (archive_path[archive_path.Length - 1] != '\\')
            {
                archive_path += '\\';
                archive_path += archive_name;
            }
            else
                archive_path = archive_path + archive_name;
            
            if (File.Exists(archive_path))
            {
                File.Delete(archive_path);
            }

            f = new FileStream(file_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            r = new FileStream(archive_path, FileMode.CreateNew);

            r.WriteByte((byte)Format.Length);

            for (int i = 0; i < Format.Length; ++i)
            {
                r.WriteByte((byte)Format[i]);
            }
            List<byte> BT = new List<byte>();

            while (f.Position < f.Length)
            {
                byte b = (byte)f.ReadByte();

                if (BT.Count == 0)
                    BT.Add(b);

                else if (BT[BT.Count - 1] != b)
                {// неповторы
                    
                    BT.Add(b);
                    
                    if (BT.Count == 255) {
                        r.WriteByte((byte)0);
                        r.WriteByte((byte)255);
                        r.Write(BT.ToArray(), 0, 255);
                        BT.Clear();
                    }
                }
                else
                {// повторы
                    if (BT.Count != 1)
                    {
                        r.WriteByte((byte)0);
                        r.WriteByte((byte)(BT.Count - 1));
                        r.Write(BT.ToArray(), 0, BT.Count - 1);
                        BT.RemoveRange(0, BT.Count - 1);
                    }
                    
                    BT.Add(b);
                    
                    while ((b = (byte)f.ReadByte()) == BT[0])
                    {
                        BT.Add(b);
                        if (BT.Count == 255)
                        {
                            r.WriteByte((byte)255);
                            r.WriteByte(BT[0]);
                            BT.Clear();
                            break;
                        }
                    }
                    
                    if (BT.Count > 0)
                    {
                        r.WriteByte((byte)BT.Count);
                        r.WriteByte(BT[0]);
                        BT.Clear();
                        BT.Add(b);
                    }
                }
            }
            
            if (BT.Count > 0)
            {
                r.WriteByte((byte)0);
                r.WriteByte((byte)BT.Count);
                r.Write(BT.ToArray(), 0, BT.Count);
            }
            
            if (f.Length < r.Length)
            {
                MessageBox.Show("С помощью данного архиватора невозможно сжать выбраный файл!");
                
                if (f != null) f.Close();
                if (r != null) r.Close();
                
                if (File.Exists(archive_path))
                {
                    File.Delete(archive_path);
                }
            }
            else
            {
                MessageBox.Show("Архивация завершена!");
                
                if (f != null) f.Close();
                if (r != null) r.Close();
            }
        }

        private void Unarchive_button_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string archive_path = openFileDialog1.FileName;
            f = new FileStream(archive_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            
            string archive_name = openFileDialog1.SafeFileName;
            string format = ".";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel) return;

            string dir = folderBrowserDialog1.SelectedPath;
            
            if (dir[dir.Length - 1] != '\\') dir += '\\';
            
            string filename = dir + archive_name.Substring(0, archive_name.LastIndexOf('.'));
            int FormatLen = f.ReadByte();
            
            for (int i = 0; i < FormatLen; ++i)
                format += (char)f.ReadByte();
            
            if (File.Exists(filename + format))
            {
                File.Delete(filename + format);
            }
            
            r = new FileStream(filename + format, FileMode.CreateNew);
            
            while (f.Position < f.Length)
            {
                int BT = f.ReadByte();
                
                if (BT == 0)
                {
                    BT = f.ReadByte();
                    
                    for (int i = 0; i < BT; i++)
                    {
                        byte b = (byte)f.ReadByte();
                        r.WriteByte(b);
                    }
                }
                else
                {
                    int Value = f.ReadByte();
                    
                    for (int i = 0; i < BT; i++)
                    {
                        r.WriteByte((byte)Value);
                    }
                }
            }

            if (f != null) f.Close();
            if (r != null) r.Close();
            
            MessageBox.Show("Разархивирование завершено!");
        }
    }
    
}
