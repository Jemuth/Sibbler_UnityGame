# ProjectSE_UnityCoderhouse
## Proyecto para curso "Desarrollo de videojuegos en Unity" Coderhouse

### Log de desarrollo - ProjectSE

**Descripcion general del proyecto**

##**Project SE**##

![](https://raw.githubusercontent.com/Jemuth/ProjectSE_UnityCoderhouse/main/Images/GDDCoverArt.jpg)

*Concepto inicial*
Juego de sigilo estrategico - puzzle objetivo es recolectar llaves y escapar del escenario, con distintos tipos de enemigos los cuales segun el tipo tendran distintos patrones de patrullaje y persecucion. El jugador controlara a dos hermanas,
las cuales poseen distintos atributos y una habilidad particular que deberan ocupar.

*Resumen de build inicial*

Juego de alta dificultad y coordinacion estilo sigilo-estrategia en el cual se debera recopilar llaves y encontrar la salida, evitando el campo de vision de los enemigos. El jugador controla a dos personajes intercambiables: dos hermanas que deben escapar de las criaturas
sombrias. Cada una posee distintos skills y atributos que deberan usar estrategicamente para escapar. El juego posee una direccion de arte y estilo visual que intenta emular juegos de la era del PSX, utilizando shader, por ejemplo, con 
vertex jittering y texturas de baja resolucion. El diseño de los escenarios se penso utilizando un sistema de grilla utilizando la escala de 1x1x1, y modelando escenario en Maya utilizando una cuadricula.
El mayor desafio fue implementar mecanicas que involucraran a distintas entidades en la escena, al mismo tiempo que cada jugador maneja sus propias acciones, cooldowns o eventos.

*Controles*

- Input directinal : Movimiento AWSD + Mouse Look
- Left shift : Run(Vigilar stamina!)
- C: Cambio de jugador: El jugador inactivo aparecera en la camara inferior derecha, lo cual ayudara a evitar peligro y coordinar acciones. Sirve a modo de minimap para evaluar la situacion.
- E: Skill especial(Posee cooldown: 
- Hermana mayor(P1) golpea a los enemigos tipo VIGILANTES(ojos rojos) por la espalda con su bat.Mientras esten deshabilitados, mostrara un icono. Posee cooldown de baja duracion.
- Habilidad pasiva: Puede correr mucho mas rapido y por mayor tiempo lo cual le ayuda a escapar a tiempo del FoV enemigo. Puede destruir objetos que obstaculizan el paso(en implementacion)
- Hermana menor(P2) deja un oso de peluche que distrae a los enemigos de tipo PATRULLEROS(ojos amarillos). Todo patrullero que entre en contacto dejara de moverse por el tiempo de su duracion. Cooldown de alta duracion
- Habilidad pasiva: Puede escabullirse por tuneles y encontrar objetos claves o tomar atajos. Mas dificil de ver, pero corre mas lento y se cansa mas rapido.

*Bugs o modificaciones a realizar identificadas**

- UI solamente funcional(incompleta a nivel de acabo)
- El sistema de indicacion de FoV se realizo pensando en la simulacion de spotlight que usaba el hardware de la PSX(mesh con transparencia y emision).Sin embargo esto satura la pantalla con demasiado informacion visual.En futuro
se plantea utilizar un Sprite con un audio cue para indicar deteccion.
- El sistema de deteccion de golpe de la habilidad de bat puede ser irresponsivo en raras ocasiones, dependiendo de la ubicacion de los colliders.

*Otros detalles y agradecimientos*

Todos los modelos de personajes, escenario y otros assets, asi como las texturas texturas son hechos o modificados por mi. Animaciones de Mixamo. Shader URP PSX por Kodrin.Musica de escena 1 y escena 2 por Lisergishnu.
Juego sin fin comercial.

#### Bitacora de desarrollo

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

**Desarrollo entrega final**

**07/06/2023**

1. Creacion de nuevo repositorio para replanteamiento del juego acorde a las exigencias de la entrega final
2. Se incorpora arte conceptual / Game cover art

**08/06/2023**

1. Creacion de mesh de testeo estilo low poly(direccion de arte del juego)
2. Actualizacion gitignore

**09/06/2023**

1. Creacion mesh P1 con sus respectivos UV
2. Implementacion de texturas

**10/06/2023**

1.Personaje P1 completado con sus respectivas texturas

**12/06/2023**

1. Modelo y texturas completas de personaje P1 y P2 
2. Creacion de script de movimiento y barra de stamina con atributos(velocidad, stamina maxima) otorgados con scriptable objects
3. Incorporacion de elemento UI de barra de stamina

**13/06/2023

1. Implementacion de scripts de movimiento y respectivas animaciones(Idle-Walk)
2. Implementacion de animacion de correr y barra de stamina asociada

**14/06/2023

1. Implementacion completa de script de movimiento basico
2. Animaciones relativas a movimiento con ciclo actualizado de animaciones(idle,walk,run,tired)
3. Aplicacion de Lerp y otras funciones y clases para mejorar fluidez de movimiento

**15/06/2023

1. Implementacion de cambio de personaje completada(se arreglan fallas de transicion)
2. Implementacion de sistema de camera follow para ambos personajes
3. Se añade sistema de camaras Cinemachine con su respectivo script para cambio de camara segun personaje seleccionado

** BITACORA A ACTUALIZAR POSTERIOR A BUILD INICIAL

