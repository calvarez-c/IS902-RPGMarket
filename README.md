# RPGMarket (In-Game Marketplace)
**Proyecto para la clase de Sistemas Expertos**

---

## Descripción del Proyecto
**RPGMarket** es una plataforma web orientada al comercio de ítems virtuales para videojuegos de rol (RPG)[cite: 1]. El sistema funciona como una casa de subastas y mercado centralizado donde los jugadores pueden listar, comprar y vender artículos virtuales (como armas, armaduras y consumibles) utilizando exclusivamente la moneda interna del juego (*"Gold"*).

Además, el sistema integra control de acceso por roles (administradores y jugadores) y mecanismos de regulación económica para supervisar las transacciones y el catálogo global del juego.

---

## Objetivo
Desarrollar una aplicación web transaccional y modular que permita aplicar principios de arquitectura limpia, gestión de identidades seguras y consistencia en operaciones financieras virtuales.

---

## Alcance del Proyecto (MVP)
El proyecto abarcará el flujo esencial de un mercado virtual seguro:

* **Autenticación y Roles (IAM):** Registro e inicio de sesión seguro mediante tokens JWT, diferenciando permisos entre jugadores (`Player`) y administradores (`Admin`).
* **Billetera Virtual Inicial:** Asignación automática de saldo en oro virtual a cada jugador para operar en el mercado.
* **Catálogo Global de Ítems (Backoffice):** Panel de administración protegido para crear, modificar, listar y eliminar ítems base del juego (armas, armaduras, consumibles, materiales).
* **Publicación de Ofertas (Marketplace):** Funcionalidad para que los jugadores pongan a la venta sus objetos fijando precios personalizados.
* **Motor Transaccional Atómico:** Lógica de compra que valida saldos, transfiere el oro del comprador al vendedor, actualiza el estado de la publicación y cambia la propiedad del ítem de forma segura y consistente.
* **Historial y Perfil:** Consulta de balance actual e historial de compras y ventas realizadas por el usuario.

---

## Fuera de Alcance
Las siguientes características no forman parte de este alcance inicial:
* Subastas dinámicas con pujas en tiempo real mediante WebSockets.
* Pasarelas de pago con dinero real (monetización fiat o tarjetas de crédito).
* Chat interno o mensajería directa entre jugadores.
