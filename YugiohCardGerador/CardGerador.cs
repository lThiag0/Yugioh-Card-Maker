using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YugiohCardGerador
{
    public partial class CardGerador : Form
    {
        PictureBox[] pictureBoxs = new PictureBox[13];
        public CardGerador()
        {
            InitializeComponent();
            CardBox.Controls.Add(TituloCard);
            CardBox.Controls.Add(AtributePictureBox);
            CardBox.Controls.Add(level1PictureBox);
            CardBox.Controls.Add(level2PictureBox);
            CardBox.Controls.Add(level3PictureBox);
            CardBox.Controls.Add(level4PictureBox);
            CardBox.Controls.Add(level5PictureBox);
            CardBox.Controls.Add(level6PictureBox);
            CardBox.Controls.Add(level7PictureBox);
            CardBox.Controls.Add(level8PictureBox);
            CardBox.Controls.Add(level9PictureBox);
            CardBox.Controls.Add(level10PictureBox);
            CardBox.Controls.Add(level11PictureBox);
            CardBox.Controls.Add(level12PictureBox);
            CardBox.Controls.Add(level13PictureBox);
            CardBox.Controls.Add(MainPictureBox);
            CardBox.Controls.Add(TipoTextBox);
            CardBox.Controls.Add(DescrTextBox);
            CardBox.Controls.Add(ValorTextBox);
            CardBox.Controls.Add(InfoTextBox);

            AlingTituloCardInput.SelectedIndex = 0;
            attributesComboBox.SelectedIndex = 0;
            TipoDeCard.SelectedIndex = 0;
            AtualizarValorCard();
            InfoCardInput.Text = "© Copyright: ";

            pictureBoxs[0] = level1PictureBox;
            pictureBoxs[1] = level2PictureBox;
            pictureBoxs[2] = level3PictureBox;
            pictureBoxs[3] = level4PictureBox;
            pictureBoxs[4] = level5PictureBox;
            pictureBoxs[5] = level6PictureBox;
            pictureBoxs[6] = level7PictureBox;
            pictureBoxs[7] = level8PictureBox;
            pictureBoxs[8] = level9PictureBox;
            pictureBoxs[9] = level10PictureBox;
            pictureBoxs[10] = level11PictureBox;
            pictureBoxs[11] = level12PictureBox;
            pictureBoxs[12] = level13PictureBox;
        }

        private void ExportarCard_Click(object sender, EventArgs e)
        {
            Bitmap bp = new Bitmap(CardBox.Width, CardBox.Height);
            CardBox.DrawToBitmap(bp, new Rectangle(0, 0, 5900, 8600));

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            ImageFormat format = ImageFormat.Jpeg;

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bp.Save(sfd.FileName, format);
                MessageBox.Show("O card foi salvo com sucesso!");
            }
        }

        private void CorDoTituloCard_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = false;
            MyDialog.Color = TituloCard.ForeColor;

            if (MyDialog.ShowDialog() == DialogResult.OK)
                TituloCard.ForeColor = MyDialog.Color;
        }

        private void FonteDoTituloCard_Click(object sender, EventArgs e)
        {
            FontDialog MyFontDialog = new FontDialog();
            MyFontDialog.ShowColor = true;

            MyFontDialog.Font = TituloCard.Font;
            MyFontDialog.Color = TituloCard.ForeColor;

            if (MyFontDialog.ShowDialog() != DialogResult.Cancel)
            {
                TituloCard.Font = MyFontDialog.Font;
                TituloCard.ForeColor = MyFontDialog.Color;
            }
        }

        private void AlingTituloCardInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(AlingTituloCardInput.SelectedIndex == 0)
            {
                TituloCard.TextAlign = ContentAlignment.MiddleLeft;
            } 
            else if(AlingTituloCardInput.SelectedIndex == 1)
            {
                TituloCard.TextAlign = ContentAlignment.MiddleCenter;
            } 
            else
            {
                TituloCard.TextAlign = ContentAlignment.MiddleRight;
            }
        }

        private void TituloCardInput_TextChanged(object sender, EventArgs e)
        {
            TituloCard.Text = TituloCardInput.Text;
        }

        private void attributesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = "../Pics/Attributes/" + attributesComboBox.Text.ToLower() + ".png";
            AtributePictureBox.Image = Image.FromFile(path);
        }

        private void ranklevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 13; i++)
            {
                pictureBoxs[i].Image = null;
            }

            if (ranklevelComboBox.Text == "Level")
            {
                StarUpDown.Value = 13;
                StarUpDown.Enabled = true;
                ResetStar.Enabled = true;
            }
            else if (ranklevelComboBox.Text == "Rank")
            {
                StarUpDown.Value = 0;
                StarUpDown.Enabled = true;
                ResetStar.Enabled = true;
            }
            else if (ranklevelComboBox.Text == "Sem Stars")
            {
                StarUpDown.Value = 0;
                StarUpDown.Enabled = false;
                ResetStar.Enabled = false;
            }
        }

        private void StarUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ranklevelComboBox.Text != "")
            {
                int rankLevel = (int)StarUpDown.Value;
                string rankLevelPath = "../Pics/Star/" + ranklevelComboBox.Text.ToLower() + ".png";

                if (ranklevelComboBox.Text == "Level")
                {
                    for (int i = 12; i >= rankLevel; i--)
                    {
                        pictureBoxs[i].Image = Image.FromFile(rankLevelPath);
                    }
                }
                else if (ranklevelComboBox.Text == "Rank")
                {
                    for (int i = 0; i < rankLevel; i++)
                    {
                        pictureBoxs[i].Image = Image.FromFile(rankLevelPath);
                    }
                }
                else
                {
                    for (int i = 0; i < 13; i++)
                    {
                        pictureBoxs[i].Image = null;
                    }
                }
            } else {
                MessageBox.Show("Escolha o tipo de rank!"); 
            }
        }

        private void ResetStar_Click(object sender, EventArgs e)
        {
            if (ranklevelComboBox.Text == "Level")
            {
                StarUpDown.Value = 13;
            }
            else if (ranklevelComboBox.Text == "Rank")
            {
                StarUpDown.Value = 0;
            }
            for (int i = 0; i < 13; i++)
            {
                pictureBoxs[i].Image = null;
            }
        }

        private void TipoDeCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = "../Pics/Cards/" + TipoDeCard.Text.ToLower() + ".png";
            CardBox.BackgroundImage = Image.FromFile(path);

            if (TipoDeCard.Text == "Normal" || TipoDeCard.Text == "Effect" || TipoDeCard.Text == "Ritual" || TipoDeCard.Text == "Fusion" || TipoDeCard.Text == "Synchro")
            {
                StarUpDown.Enabled = true;
                ranklevelComboBox.Enabled = true;
                ResetStar.Enabled = true;
            }
            else if (TipoDeCard.Text == "Xyz")
            {
                StarUpDown.Enabled = true;
                ranklevelComboBox.Enabled = false;
                ResetStar.Enabled = true;

                TituloCard.ForeColor = Color.White;
                ranklevelComboBox.SelectedIndex = 1;
            }
            else if (TipoDeCard.Text == "Spell" || TipoDeCard.Text == "Trap")
            { 
                StarUpDown.Enabled = false;
                ranklevelComboBox.Enabled = false;
                ResetStar.Enabled = false;
            }

            AtualizarCategoriaCard();
        }

        private void UploadImageCard_Click(object sender, EventArgs e)
        {
            OpenFileDialog image = new OpenFileDialog();
            image.Filter = "jpg files(*.jpg)|*.jpg| PNG Files(*.png)|*.png| All Files(*.*)|*.*";

            if (image.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = image.FileName;

                MainPictureBox.Image = Image.FromFile(filePath);
            }
        }

        private void DescCardInput_TextChanged(object sender, EventArgs e)
        {
            DescrTextBox.Text = DescCardInput.Text;
        }

        private void FonteDaDescCard_Click(object sender, EventArgs e)
        {
            FontDialog MyFontDialog = new FontDialog();
            MyFontDialog.ShowColor = true;

            MyFontDialog.Font = DescrTextBox.Font;
            MyFontDialog.Color = DescrTextBox.ForeColor;

            if (MyFontDialog.ShowDialog() != DialogResult.Cancel)
            {
                DescrTextBox.Font = MyFontDialog.Font;
                DescrTextBox.ForeColor = MyFontDialog.Color;
            }
        }

        private void CorDaDescCard_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = false;
            MyDialog.Color = DescrTextBox.ForeColor;

            if (MyDialog.ShowDialog() == DialogResult.OK)
                DescrTextBox.ForeColor = MyDialog.Color;
        }

        private void MonsterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarCategoriaCard();
        }

        private void ExtracomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarCategoriaCard();
        }

        private void AtualizarCategoriaCard() {
            if(MonsterComboBox.Text == "" && ExtracomboBox.Text == "")
            {
                TipoTextBox.Text = "[ " + TipoDeCard.Text + " ]";
            }
            else if(MonsterComboBox.Text != "" && ExtracomboBox.Text == "")
            {
                TipoTextBox.Text = "[ " + MonsterComboBox.Text + " / " + TipoDeCard.Text + " ]";
            }
            else if(MonsterComboBox.Text == "" && ExtracomboBox.Text != "")
            {
                TipoTextBox.Text = "[ " + TipoDeCard.Text + " / " + ExtracomboBox.Text + " ]";
            }
            else
            {
                TipoTextBox.Text = "[ " + MonsterComboBox.Text + " / " + TipoDeCard.Text + " / " + ExtracomboBox.Text + " ]";
            }
        }

        private void LimparCategoriaCard_Click(object sender, EventArgs e)
        {
            MonsterComboBox.SelectedIndex = -1;
            ExtracomboBox.SelectedIndex = -1;
            AtualizarCategoriaCard();
        }

        private void AtkCardInput_ValueChanged(object sender, EventArgs e)
        {
            AtualizarValorCard();
        }

        private void DefCardInput_ValueChanged(object sender, EventArgs e)
        {
            AtualizarValorCard();
        }

        private void AtualizarValorCard()
        {
            ValorTextBox.Text = "ATK / " + AtkCardInput.Text + "   DEF / " + DefCardInput.Text;
        }

        private void InfoCardInput_TextChanged(object sender, EventArgs e)
        {
            InfoTextBox.Text = InfoCardInput.Text;
        }

        private void LimpaCard_Click(object sender, EventArgs e)
        {
            TituloCardInput.Text = "";
            AlingTituloCardInput.SelectedIndex = 0;
            attributesComboBox.SelectedIndex = 0;
            TipoDeCard.SelectedIndex = 0;
            MonsterComboBox.SelectedIndex = -1;
            ExtracomboBox.SelectedIndex = -1;
            MainPictureBox.Image = null;
            DescCardInput.Text = "";
            for (int i = 0; i < 13; i++)
            {
                pictureBoxs[i].Image = null;
            }
            StarUpDown.Value = 0;
            ranklevelComboBox.SelectedIndex = -1;
            AtkCardInput.Value = 0;
            DefCardInput.Value = 0;
            AtualizarValorCard();
            InfoCardInput.Text = "© Copyright: ";
        }
    }
}
