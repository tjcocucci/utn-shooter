## Parte 2

- No tuve mucho tiempo así que intenté implementar las features pero sin mucha dedicación a la parte estética (puse sonidos pero si elegirlos super bien, hice la UI y responde correctamente pero no es linda de ver, etc.)
- Pude refactorear el código para que la funcionalidad de disparar sea un componente aparte que se comparte entre enemigos y el jugador
- La dificultad de los enemigos es progresiva, no solo en la velocidad y el arma que usan sino que también en que tienen distintos esquemas de movimiento (enemigos fáciles se mueven de a intervalos, los normales de manera continua y los difíciles hacen un flanqueo al jugador)
- Más allá de que el juego no está muy lindo me parece que pude mejorar bastante la calidad del código.

## Pendientes, Inquietudes y Meaculpas

- Creo que el Destroy en el EquipWeapon tira un error (Destroying assets is not permitted to avoid data loss.) pero no se cual sería la implementación correcta
- No estoy seguro si poner un AudioSource para cada sonido es una buena práctica
- Balance del juego mejorado pero no necesariamente perfeccionado. Capaz está muy difícil
- No hice un buen tratamiento estético. Elegí colores y sonidos sin mucho criterio
