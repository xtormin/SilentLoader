# SilentLoader

SilentLoader es una herramienta que implementa la carga y ejecución de ensamblados .NET directamente en memoria sin persistencia en disco, utilizando la API de Reflection para evadir controles de seguridad basados en monitorización de filesystem.

## Funcionamiento

1. Carga en Memoria: En lugar de ejecutar un archivo directamente desde el disco, SilentLoader lee todo el contenido del archivo .NET (como Rubeus.exe) y lo carga completamente en la memoria RAM del proceso.
2. API de Reflection: Utiliza un conjunto de capacidades nativas de .NET conocidas como "Reflection". Esta API permite examinar, manipular y ejecutar código de forma dinámica en tiempo de ejecución. Específicamente:
  * Assembly.Load(byte[]): Este método carga un ensamblado completo a partir de un array de bytes, sin necesidad de tener un archivo en disco.
  * assembly.EntryPoint: Permite identificar el punto de entrada (método Main) del programa cargado.
  * Invoke(): Ejecuta el método principal del programa cargado.
3. Sin Persistencia: A diferencia de métodos tradicionales como Process.Start() que requieren que el archivo exista en disco durante toda la ejecución, SilentLoader solo necesita acceso momentáneo al archivo para leerlo y luego puede ejecutarlo puramente desde la memoria.

## Técnicas de evasión implementadas

- Pausa aleatoria mediante `Thread.Sleep()` para eludir análisis automatizado.
- Nombres genéricos para evitar detección por `strings`.
- Manejo silencioso de excepciones para no dejar rastros en logs.
- Compatibilidad con .NET Framework 4.0+ para entornos legacy.

## Uso

### Sintaxis
```
SilentLoader.exe -f [ruta_programa.exe] [-p "argumentos opcionales"]
```

### Ejemplos

```bash
# Ejecución básica
SilentLoader.exe -f Rubeus.exe

# Con argumentos
SilentLoader.exe -f Rubeus.exe -p "kerberoast /outfile:hash.txt"
SilentLoader.exe -f SharpHound.exe -p "-c All"
```

## Compilación

```powershell
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /out:SilentLoader.exe /target:exe /reference:System.dll /reference:System.Core.dll SilentLoader.cs
```

## Consideraciones

- Modificar código fuente antes de cada operación.
- Usar nombres de archivo genéricos.
- Combinar con otras técnicas de evasión cuando sea necesario.