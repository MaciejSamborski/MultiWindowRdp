using System;

namespace MultiWindowRdp
{
    partial class RdpForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RdpForm));
            this.axMsRdpClient9NotSafeForScripting2 = new AxMSTSCLib.AxMsRdpClient9NotSafeForScripting();
            ((System.ComponentModel.ISupportInitialize)(this.axMsRdpClient9NotSafeForScripting2)).BeginInit();
            this.SuspendLayout();
            // 
            // axMsRdpClient9NotSafeForScripting2
            // 
            this.axMsRdpClient9NotSafeForScripting2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMsRdpClient9NotSafeForScripting2.Enabled = true;
            this.axMsRdpClient9NotSafeForScripting2.Location = new System.Drawing.Point(0, 0);
            this.axMsRdpClient9NotSafeForScripting2.Margin = new System.Windows.Forms.Padding(0);
            this.axMsRdpClient9NotSafeForScripting2.Name = "axMsRdpClient9NotSafeForScripting2";
            this.axMsRdpClient9NotSafeForScripting2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMsRdpClient9NotSafeForScripting2.OcxState")));
            this.axMsRdpClient9NotSafeForScripting2.Size = new System.Drawing.Size(770, 439);
            this.axMsRdpClient9NotSafeForScripting2.TabIndex = 0;
            // 
            // RdpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 439);
            this.Controls.Add(this.axMsRdpClient9NotSafeForScripting2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RdpForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.RdpForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axMsRdpClient9NotSafeForScripting2)).EndInit();
            this.ResumeLayout(false);

        }



        #endregion
        private AxMSTSCLib.AxMsRdpClient9NotSafeForScripting axMsRdpClient9NotSafeForScripting2;
    }
}

