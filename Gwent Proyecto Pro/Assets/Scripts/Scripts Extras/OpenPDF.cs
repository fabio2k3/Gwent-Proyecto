using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class OpenPDF : MonoBehaviour
{
    public string pdfFilePath = @"D:\Fabio_2k3\Escuela\Progamación\Proyectos\Pdf Proyecto\Informe Proyecto Programación - GWENT 1.0.pdf";

    public void OpenPDFFile()
    {
        // Verificar si el archivo PDF existe
        if (!System.IO.File.Exists(pdfFilePath))
        {
            UnityEngine.Debug.LogError("El archivo PDF no existe en la ruta especificada: " + pdfFilePath);
            return;
        }

        // Abrir el archivo PDF con el visor de PDF predeterminado del sistema operativo
        Process.Start(pdfFilePath);
    }
}
