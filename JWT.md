# Implementar JWT
___

JWT son las siglas de "JSON Web Token". Es un estándar abierto (RFC 7519) que define un formato compacto y autónomo para transmitir información entre dos partes de manera segura como un objeto JSON. Los JWT se utilizan comúnmente para autenticar usuarios y compartir información de manera segura en aplicaciones web y servicios.

Un JWT consta de tres partes separadas por puntos:

1. Header (Encabezado): Contiene información sobre el tipo de token (JWT) y el algoritmo de firma utilizado, generalmente se encuentra en formato JSON.
2. Payload (Carga útil): Es la parte central del JWT y contiene los datos que se desean transmitir. Puede incluir información como el ID del usuario, roles, permisos u otros datos relevantes. También se encuentra en formato JSON.

3. Signature (Firma): La firma se crea combinando el encabezado, la carga útil y una clave secreta utilizando el algoritmo de firma especificado en el encabezado. Esta firma se utiliza para verificar la autenticidad del token y garantizar que no haya sido modificado por terceros.

Los JWT son muy útiles en aplicaciones modernas debido a su capacidad para representar información de manera segura y compacta, y son ampliamente utilizados en autenticación y autorización, ya sea para permitir que los usuarios accedan a recursos protegidos o para compartir información de manera segura entre servicios y sistemas. También son portables, lo que significa que un JWT generado en un sistema puede ser verificado por otro sistema siempre que ambos compartan la misma clave secreta o utilicen un mecanismo de verificación adecuado.
