# UTN - Unos Tiritos Nomás

## Descripción

Unos Tiritos Nomás es un juego de disparos en tercera persona desarrollado en Unity 2022.3.4f1. El objetivo principal del juego es eliminar a todos los enemigos en cada nivel para avanzar y completar el juego. 

El jugador tiene la capacidad de disparar con diferentes tipos de armas. Actualmente, se han implementado una ametralladora de fuego rápido con daño bajo y un rifle más lento pero más poderoso.

La interfaz registra el tiempo de juego, la salud del jugador, el número de enemigos eliminados, el nivel actual y el arma en uso a través en un elemento de UI.


## Desarrollo

Admitidamente yo ya había hecho un proyecto similar, siguiendo el tutorial de Sebastián Lague (https://www.youtube.com/playlist?list=PLFt_AvWsXl0ctd4dgE1F8g3uec4zKNRV0). Este proyecto lo empecé genuinamente de cero pero algunas cosas de lo que había hecho antes miré. 

El repositorio está subido en https://github.com/tjcocucci/utn-shooter

## Controles

- Apuntar: posición del mouse
- Disparar: click izquierdo
- Arma primaria: 1
- Arma secundaria: 2
- Movimiento: WASD o flechas

## Características

- Generación Aleatoria de Enemigos: los enemigos se generan de forma aleatoria y a tiempos espaciados dentro de cada nivel.
- Selección de Armas: el jugador tiene acceso a diferentes tipos de armas, creé una clase `Weapon` para que crear nuevas armas no sea demasiado trabajoso.
- Salud: tanto el jugador como los enemigos cuentan con un sistema de salud. Ambos reciben daño al recibir balazos y mueren si la salud se agota
- Desplazamiento Automático de Enemigos: los enemigos detectan al jugador y avanzan en línea recta hacia él.
- Niveles: utilizando prefabs como base, se pueden crear nuevos niveles de forma relativamente sencilla.
- Movimiento y Aim: usé GetAxis para el movimiento y el aim es con la posición del mouse y un raycast para mapear la posición del mouse en pantalla al plano del juego.
- Progresión: el objetivo es eliminar a todos los enemigos en cada nivel para avanzar al siguiente nivel. El juego se completa cuando se eliminan todos los enemigos en todos los niveles. Si morís perdés
- Eventos: usé eventos para comunicar muerte de jugador, de enemigos y cambio de nivel. No recuerdo si esto era la mejor manera de hacerlo según lo que vimos en clases
- Invokes: usé invoke para cambiar de un nivel a otro y dejar tiempo para que aparezca un cartel (no implementé el cartel pero si hay un log por consola)
- Temporizadores: hay temporizadores para separar los spawns de enemigos y para controlar los intervalos entre disparos.


## Pendientes e Inquietudes

- Lista de niveles: Actualmente, la lista de niveles se implementa como un array que debe llenarse en el editor. Esta solución puede resultar poco práctica si se quiere añadir una gran cantidad de niveles. Además cada nivel tiene ciertos requisitos, como un plano para la spawner por lo que si no se tiene cuidado ocurren errores y también puede demorar el workflow del desarrollo.
- Spawner: el spawner no tiene en cuenta la colocación de los obstáculos en el nivel. La solución actual es generar a los enemigos en una altitud superior y permitir que caigan al nivel del suelo, pero no es muy elegante.
- No hay menú inicial ni pausa.
- Escenas: no usé escenas, más que nada porque Fede recomendó no cambiar escenas para niveles chicos como estos. Podría implementar cambios de escena para el menú.
- No hay arte ni sonido
- Código duplicado: El código de disparo se repite en las clases del jugador y enemigo. Dado que estas clases ya heredan de `DamageableObject` no puedo hacer que además hereden de otra clase que abstraiga la lógica de disparo porque no hay herencia múltiple en C#. No se bien como se resuelve esto.
- Los enemigos podrían portar un arma igual que lo hace el jugador, sería más limpia la implementación y ahorraría algo de código repetido, esto es sencillo de hacer pero no lo prioricé.
- Usé un LevelManager y un EnemyManager pero no un GameManager. No se si la arquitectura que elegí será la mejor, capaz queda más prolijo usando un GameManager.
- Varios aspectos son muy prototípicos, por ejemplo habría que calibrar la vida, velocidad de enemigos, velocidad de balas, etc. para que el juego sea algo más entretenido.
