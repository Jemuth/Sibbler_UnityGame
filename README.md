# ProjectSE_UnityCoderhouse
## Proyecto para curso "Desarrollo de videojuegos en Unity" Coderhouse

### Log de desarrollo - ProjectSE

**Descripcion general del proyecto**

##**Project SE**##

![](https://raw.githubusercontent.com/Jemuth/ProjectSE_UnityCoderhouse/main/Images/Sibler1.jpg)

*Concepto inicial*
Juego de sigilo estrategico - puzzle objetivo es recolectar llaves y escapar del escenario, con distintos tipos de enemigos los cuales segun el tipo tendran distintos patrones de patrullaje y persecucion. El jugador controlara a dos hermanas,
las cuales poseen distintos atributos y una habilidad particular que deberan ocupar. El juego posee un estilo visual estilo PSX - baja resolucion.

*Resumen de build inicial*

Juego de alta dificultad y coordinacion estilo sigilo-estrategia en el cual se debera recopilar llaves y encontrar la salida, evitando el campo de vision de los enemigos. El jugador controla a dos personajes intercambiables: dos hermanas que deben escapar de las criaturas
sombrias. Cada una posee distintos skills y atributos que deberan usar estrategicamente para escapar. El juego posee una direccion de arte y estilo visual que intenta emular juegos de la era del PSX, utilizando shader, por ejemplo, con 
vertex jittering y texturas de baja resolucion. El diseño de los escenarios se penso utilizando un sistema de grilla utilizando la escala de 1x1x1, y modelando escenario en Maya utilizando una cuadricula.
El mayor desafio fue implementar mecanicas que involucraran a distintas entidades en la escena, al mismo tiempo que cada jugador maneja sus propias acciones, cooldowns o eventos.

*Controles*

![](https://raw.githubusercontent.com/Jemuth/ProjectSE_UnityCoderhouse/main/Images/Sibler2.jpg)

- Input directinal : Movimiento AWSD + Mouse Look
- Left shift : Run(Vigilar stamina!)
- C: Cambio de jugador: El jugador inactivo aparecera en la camara inferior derecha, lo cual ayudara a evitar peligro y coordinar acciones. Sirve a modo de minimap para evaluar la situacion.
- E: Skill especial(Posee cooldown: 
- Hermana mayor(P1) golpea a los enemigos tipo VIGILANTES(ojos rojos) por la espalda con su bat.Mientras esten deshabilitados, mostrara un icono. Posee cooldown de baja duracion.
- Habilidad pasiva: Puede correr mucho mas rapido y por mayor tiempo lo cual le ayuda a escapar a tiempo del FoV enemigo. Puede destruir objetos que obstaculizan el paso(en implementacion)
- Hermana menor(P2) deja un oso de peluche que distrae a los enemigos de tipo PATRULLEROS(ojos amarillos). Todo patrullero que entre en contacto dejara de moverse por el tiempo de su duracion. Cooldown de alta duracion
- Habilidad pasiva: Puede escabullirse por tuneles y encontrar objetos claves o tomar atajos. Mas dificil de ver, pero corre mas lento y se cansa mas rapido.

*Bugs identificados**

El sistema de deteccion de golpe de la habilidad de bat puede ser irresponsivo en raras ocasiones, dependiendo de la ubicacion de los colliders.


*Otros detalles y agradecimientos*

Todos los modelos de personajes, escenarios y asi como las texturas texturas son hechos o modificados por mi.Meshes de items y arboles son de sus respectivos autores. Animaciones de Mixamo. Shader URP PSX por Kodrin.Musica de escena 1 y escena 2 por Lisergishnu.

**Juego sin fin comercial**

*Capturas de gameplay*

![](https://raw.githubusercontent.com/Jemuth/ProjectSE_UnityCoderhouse/main/Images/Sibler3.jpg)

![](https://raw.githubusercontent.com/Jemuth/ProjectSE_UnityCoderhouse/main/Images/Sibler4.jpg)

![](https://raw.githubusercontent.com/Jemuth/ProjectSE_UnityCoderhouse/main/Images/Sibler5.jpg)

**Link a build**

https://drive.google.com/file/d/1mj0FWeTwer-bl1V6LyiAjZ27bhZcAlZJ/view?usp=drive_link

**Resumen primera entrega**

Link a repositorio de primera entrega: https://github.com/Jemuth/proyectounitycoderjjg

![](https://github.com/Jemuth/proyectounitycoderjjg/blob/main/gifs/entrega1.gif)

Se implemento acorde a los requisitos de entrega:

- Inputs basicos de movimiento para el jugador, opcion de correr al presionar tecla y funcion de agacharse para futuras mecanicas.
- Personajes con animaciones: Animaciones de jugador y enemigo.
- Sistema de camaras: Jugador con camera de seguimiento en tercera persona.
- Luces: Skybox, luz direccional y luces de ambientacion.
- Objetos con materiales: Escena y objetos de decoracion con sus respectivas texturas.
- Prefabs: Jugador, enemigos y objetos en la escena.
- Calculos vectoriales: Implementacion de sistema de patrullaje rudimentario para el enemigo.
- 1 caso de switch: Switch para determinar el tipo de enemigo el cual afecta su velocidad.
- 2 casos de temporizadores: Corrutina para determinar tiempo de patrullaje de enemigo. Sistema de stamina para jugador.
- 3 tipo de colisiones: Colliders para jugador, enemigo y objetos en escena. Sistema de colision para luz de deteccion de movimiento.

**Controles de jugador**

Movimiento WASD + mouse look. Left Shift para correr(cooldown acorde a stamina). Left Ctrl para agacharse.

**03/04/2023**

1. Creacion de proyecto con sus respectivos directorios.
2. Implementacion de git con el respectivo archivo git.ignore segun requerimientos.
3. Integracion a GitHub para posible desarrollo conjunto.

**04/04/2023**

1. Configuracion de personaje protagonista(modelo y animaciones Mixamo).
2. Creacion de script de movimiento basico.
3. Implementacion de animation controller con animaciones correspondientes al movimiento.
4. Configuracion de iluminacion de escena y Skybox de ambientacion
5. Integracion de escena y elementos decorativos(prefabs), con sus respectivas texturas
6. Implementacion de NPC, con sus respectivas animaciones
7. Creacion de script de patrullaje para NPC, con su respectivo switch para configurar tipo de enemigo(velocidad) y corrutinas para temporizacion

**05/04/2023**

1. Implementacion de sistema rudimentaria de stamina con su respectivo temporizador.
2. Creacion de script de movimiento basico.
3. Creacion de script para luz con deteccion de movimiento



