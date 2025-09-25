# Configuración del Servicio de Email

## Configuración de Gmail para envío de comprobantes

Para que el sistema pueda enviar comprobantes por correo electrónico, necesitas configurar las credenciales de Gmail en el archivo `App.config`.

### Pasos para configurar Gmail:

1. **Habilitar la verificación en dos pasos en tu cuenta de Gmail:**
   - Ve a tu cuenta de Google
   - Seguridad → Verificación en dos pasos
   - Actívala si no la tienes habilitada

2. **Generar una contraseña de aplicación:**
   - Ve a tu cuenta de Google
   - Seguridad → Verificación en dos pasos → Contraseñas de aplicaciones
   - Selecciona "Correo" y "Computadora Windows"
   - Copia la contraseña generada (16 caracteres)

3. **Configurar el archivo App.config:**
   - Abre el archivo `PITecnomovil\App.config`
   - Modifica las siguientes líneas:

```xml
<appSettings>
    <add key="EmailFrom" value="tu-email@gmail.com" />
    <add key="EmailPassword" value="contraseña-de-aplicacion-de-16-caracteres" />
    <add key="CompanyName" value="Tecnomóvil" />
</appSettings>
```

### Ejemplo de configuración:

```xml
<appSettings>
    <add key="EmailFrom" value="tecnomovil.ventas@gmail.com" />
    <add key="EmailPassword" value="abcd efgh ijkl mnop" />
    <add key="CompanyName" value="Tecnomóvil" />
</appSettings>
```

### Notas importantes:

- **NO uses tu contraseña normal de Gmail**, siempre usa una contraseña de aplicación
- La contraseña de aplicación tiene 16 caracteres separados por espacios
- Asegúrate de que el correo del cliente esté registrado correctamente en la base de datos
- El sistema enviará automáticamente el comprobante al completar una venta

### Solución de problemas:

- **Error de autenticación:** Verifica que la contraseña de aplicación sea correcta
- **No se envía el correo:** Verifica que el cliente tenga un correo válido registrado
- **Correo en spam:** Los primeros correos pueden llegar a spam, pide al cliente que marque como "no spam"

### Funcionalidades del comprobante:

- Se genera automáticamente al completar una venta
- Incluye información del cliente, productos, reparaciones y totales
- Formato HTML profesional con el logo de la empresa
- Se envía al correo registrado del cliente
- Se guarda la factura y el pago en la base de datos