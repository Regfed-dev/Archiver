using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace kljmhnbgfvcd
{
    public partial class Archiver : Form
    {
        public FileStream file = null;
        public FileStream arc = null;
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

            file = new FileStream(file_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            arc = new FileStream(archive_path, FileMode.CreateNew);

            arc.WriteByte((byte)Format.Length);

            for (int i = 0; i < Format.Length; ++i)
            {
                arc.WriteByte((byte)Format[i]);
            }
            List<byte> BT = new List<byte>();

            while (file.Position < file.Length)
            {
                byte bt = (byte)file.ReadByte();

                if (BT.Count == 0)
                    BT.Add(bt);

                else if (BT[BT.Count - 1] != bt)
                {// Поиск неповторяющихся элементов
                    
                    BT.Add(bt);
                    
                    if (BT.Count == 255) {
                        arc.WriteByte((byte)0);
                        arc.WriteByte((byte)255);
                        arc.Write(BT.ToArray(), 0, 255);
                        BT.Clear();
                    }
                }
                else
                {// Поиск повторяющихся элементов
                    if (BT.Count != 1)
                    {
                        arc.WriteByte((byte)0);
                        arc.WriteByte((byte)(BT.Count - 1));
                        arc.Write(BT.ToArray(), 0, BT.Count - 1);
                        BT.RemoveRange(0, BT.Count - 1);
                    }
                    
                    BT.Add(bt);
                    
                    while ((bt = (byte)file.ReadByte()) == BT[0])
                    {
                        BT.Add(bt);
                        if (BT.Count == 255)
                        {
                            arc.WriteByte((byte)255);
                            arc.WriteByte(BT[0]);
                            BT.Clear();
                            break;
                        }
                    }
                    
                    if (BT.Count > 0)
                    {
                        arc.WriteByte((byte)BT.Count);
                        arc.WriteByte(BT[0]);
                        BT.Clear();
                        BT.Add(bt);
                    }
                }
            }
            
            if (BT.Count > 0)
            {
                arc.WriteByte((byte)0);
                arc.WriteByte((byte)BT.Count);
                arc.Write(BT.ToArray(), 0, BT.Count);
            }
            
            if (file.Length < arc.Length)
            {
                MessageBox.Show("С помощью данного архиватора невозможно сжать выбраный файл!");
                
                if (file != null) file.Close();
                if (arc != null) arc.Close();
                
                if (File.Exists(archive_path))
                {
                    File.Delete(archive_path);
                }
            }
            else
            {
                MessageBox.Show("Архивация завершена!");
                
                if (file != null) file.Close();
                if (arc != null) arc.Close();
            }
        }

        private void Unarchive_button_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string archive_path = openFileDialog1.FileName;
            file = new FileStream(archive_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            
            string archive_name = openFileDialog1.SafeFileName;
            string format = ".";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel) return;

            string dir = folderBrowserDialog1.SelectedPath;
            
            if (dir[dir.Length - 1] != '\\') dir += '\\';
            
            string filename = dir + archive_name.Substring(0, archive_name.LastIndexOf('.'));
            int FormatLen = file.ReadByte();
            
            for (int i = 0; i < FormatLen; ++i)
                format += (char)file.ReadByte();
            
            if (File.Exists(filename + format))
            {
                File.Delete(filename + format);
            }

            arc = new FileStream(filename + format, FileMode.CreateNew);
            
            while (file.Position < file.Length)
            {
                int BT = file.ReadByte();
                
                if (BT == 0)
                {
                    BT = file.ReadByte();
                    
                    for (int i = 0; i < BT; i++)
                    {
                        byte bt = (byte)file.ReadByte();
                        arc.WriteByte(bt);
                    }
                }
                else
                {
                    int Value = file.ReadByte();
                    
                    for (int i = 0; i < BT; i++)
                    {
                        arc.WriteByte((byte)Value);
                    }
                }
            }

            if (file != null) file.Close();
            if (arc != null) arc.Close();
            
            MessageBox.Show("Разархивирование завершено!");
        }
    }
    
}
