# proyectounitycoderjjg
## Proyecto para curso Unity Coderhouse

### Log de desarrollo - Juego de sigilo(nombre aun no decidido)

**Descripcion general del proyecto**

Juego de sigilo-thriller cuyo objetivo es recolectar distintos objetos y escapar del escenario, con distintos tipos de enemigos los cuales segun el tipo tendran distintos patrones de patrullaje y persecucion. El jugador encontrara zonas en las cuales
refugiarse y objetos que lo ayudaran en su objetivo.

#### Bitacora de desarrollo

**Resumen primera entrega**

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