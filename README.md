# ProjectSE_UnityCoderhouse
## Proyecto para curso "Desarrollo de videojuegos en Unity" Coderhouse

### Log de desarrollo - ProjectSE

**Descripcion general del proyecto**

##**Project SE**##

![](https://raw.githubusercontent.com/Jemuth/ProjectSE_UnityCoderhouse/main/Images/GDDCoverArt.jpg)

Juego de sigilo-thriller cuyo objetivo es recolectar distintos objetos y escapar del escenario, con distintos tipos de enemigos los cuales segun el tipo tendran distintos patrones de patrullaje y persecucion. El jugador controlara a dos personajes, 
los cuales poseen habilidades unicas y distintas uno del otro. El exito del escape depende del uso correcto de cada habilidad y personaje en la situacion pertinente

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

