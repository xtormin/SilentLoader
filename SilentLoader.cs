/*
SilentLoader by Xtormin
Compilación:  C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /out:SilentLoader.exe /target:exe /reference:System.dll /reference:System.Core.dll SilentLoader.cs
Uso: SilentLoader.exe -f programa.exe -p parametros
*/
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace FileUtil
{
    class ResourceManager
    {
        static void Main(string[] args)
        {
            // Verificación mínima de argumentos
            if (args.Length < 2 || args[0] != "-f")
            {
                Console.WriteLine("Uso: SilentLoader.exe -f programa.exe [-p \"parámetros opcionales\"]");
                return;
            }

            string rutaArchivo = args[1];
            string parametros = "";

            // Procesar argumentos opcionales
            for (int i = 2; i < args.Length; i++)
            {
                if (args[i] == "-p" && i + 1 < args.Length)
                {
                    parametros = args[i + 1];
                    break;
                }
            }

            // Verificación de existencia del archivo
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("Archivo no encontrado.");
                return;
            }

            try
            {
                // Pequeña pausa aleatoria
                Thread.Sleep(new Random().Next(300, 800));
                
                // Leer archivo
                byte[] datos = File.ReadAllBytes(rutaArchivo);
                
                // Cargar assembly
                Assembly ensamblado = Assembly.Load(datos);
                
                if (ensamblado == null)
                {
                    return;
                }
                
                // Obtener punto de entrada
                MethodInfo metodoPrincipal = ensamblado.EntryPoint;
                
                if (metodoPrincipal == null)
                {
                    return;
                }

                // Preparar parámetros
                object[] parametrosProcesados = null;
                
                // Solo crear array de parámetros si el método lo requiere
                if (metodoPrincipal.GetParameters().Length > 0)
                {
                    // Si no hay parámetros especificados pero el método los requiere, pasar array vacío
                    if (string.IsNullOrEmpty(parametros))
                    {
                        parametrosProcesados = new object[] { new string[0] };
                    }
                    else
                    {
                        parametrosProcesados = new object[] { parametros.Split(' ') };
                    }
                }
                
                // Invocar el método principal
                metodoPrincipal.Invoke(null, parametrosProcesados);
            }
            catch
            {
                // Salida silenciosa en caso de error
            }
        }
    }
}