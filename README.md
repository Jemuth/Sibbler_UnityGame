# ProjectSE_Unity game
**SIBLER A.K.A. Project SE**

![](https://raw.githubusercontent.com/Jemuth/ProjectSE_UnityCoderhouse/main/Images/Sibler1.jpg)

*Concepto inicial*
Sibler es un juego de sigilo estrategico - puzzle objetivo es recolectar llaves y escapar del escenario, con distintos tipos de enemigos los cuales segun el tipo tendran distintos patrones de patrullaje y persecucion. El jugador controlara a dos hermanas,
las cuales poseen distintos atributos y una habilidad particular que deberan ocupar. El juego posee un estilo visual estilo PSX - baja resolucion.

*Initial concept*
Sibler is a strategic stealth-puzzle game, the objective is to collect keys and escape from the stage, with different types of enemies, which depending on the type will have different patrolling and chasing patterns. The player will control two sisters,
which have different attributes and a particular ability that they must occupy. The game has a PSX style visual style - low resolution.

*Resumen de build inicial*

Juego de alta dificultad y coordinacion estilo sigilo-estrategia en el cual se debera recopilar llaves y encontrar la salida, evitando el campo de vision de los enemigos. El jugador controla a dos personajes intercambiables: dos hermanas que deben escapar de las criaturas
sombrias. Cada una posee distintos skills y atributos que deberan usar estrategicamente para escapar. El juego posee una direccion de arte y estilo visual que intenta emular juegos de la era del PSX, utilizando shader, por ejemplo, con 
vertex jittering y texturas de baja resolucion. El diseño de los escenarios se penso utilizando un sistema de grilla utilizando la escala de 1x1x1, y modelando escenario en Maya utilizando una cuadricula.
El mayor desafio fue implementar mecanicas que involucraran a distintas entidades en la escena, al mismo tiempo que cada jugador maneja sus propias acciones, cooldowns o eventos.

*Initial build summary*

High difficulty and coordination stealth-strategy style game in which you must collect keys and find the exit, avoiding the enemies' field of vision. The player controls two interchangeable characters: two sisters who must escape from shadowy creatures.
creatures. Each has different skills and attributes that must be used strategically to escape. The game has an art direction and visual style that tries to emulate games from the PSX era, using shader, for example, with 
vertex jittering and low resolution textures. The scenery design was thought using a grid system using 1x1x1 scale, and modeling scenery in Maya using a grid.
The biggest challenge was to implement mechanics that involved different entities in the scene, while each player handles his own actions, cooldowns or events.

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

*Controls*

- Input directinal : AWSD Movement + Mouse Look
- Left shift : Run(Watch stamina!)
- C: Player shift: The inactive player will appear in the lower right camera, which will help to avoid danger and coordinate actions. It serves as a minimap to evaluate the situation.
- E: Special Skill(Has cooldown: 
- Big Sister(P1) hits VIGILANTES(red eyes) type enemies in the back with her bat.While they are disabled, it will show an icon. Has low cooldown duration.
- Passive ability: He can run much faster and longer which helps him escape from enemy FoV in time. Can destroy objects that obstruct the passage (in implementation).
- Little Sister(P2) leaves a teddy bear that distracts the patrol type enemies (yellow eyes). Any patroller that comes in contact will stop moving for the duration of its duration. High duration cooldown
- Passive ability: Can sneak through tunnels and find key objects or take shortcuts. Harder to see, but runs slower and tires faster.


*Bugs identificados**

El sistema de deteccion de golpe de la habilidad de bat puede ser irresponsivo en raras ocasiones, dependiendo de la ubicacion de los colliders.

*Known bugs*

The hit detection system of the bat skill can be irresponsive on rare occasions, depending on the location of the colliders.

*Otros detalles y agradecimientos*

Todos los modelos de personajes, escenarios y asi como las texturas texturas son hechos o modificados por mi.Meshes de items y arboles son de sus respectivos autores. Animaciones de Mixamo. Shader URP PSX por Kodrin.Musica de escena 1 y escena 2 por Lisergishnu.

*Other details and acknowledgements*

All character models, scenery and textures are made or modified by me. Meshes of items and trees are from their respective authors. Animations by Mixamo. Shader URP PSX by Kodrin. Scene 1 and scene 2 music by Lisergishnu.

**Juego sin fin comercial**
**Non-commercial game**

*Capturas de gameplay*
*Gameplay captures*

![](https://raw.githubusercontent.com/Jemuth/ProjectSE_UnityCoderhouse/main/Images/Sibler3.jpg)

![](https://raw.githubusercontent.com/Jemuth/ProjectSE_UnityCoderhouse/main/Images/Sibler4.jpg)

![](https://raw.githubusercontent.com/Jemuth/ProjectSE_UnityCoderhouse/main/Images/Sibler5.jpg)

**Link a build**
**Link to build**

https://drive.google.com/file/d/1mj0FWeTwer-bl1V6LyiAjZ27bhZcAlZJ/view?usp=drive_link




