# IAV Final
# Curso 2020 / 2021 - Universidad Complutense de Madrid
# Facultad de Informática - Videojuegos

 Miembros:
  * Nicolás Fernández Descalzo
  * Antonio Jesús Guerra Garduño

<a href="https://drive.google.com/file/d/17gqN8tPGfN0on9QVpElQzNy5oUaQNwXt/view?usp=sharing" target="_blank">Video Demo</a>

<a href="https://drive.google.com/file/d/1YKqnutRc1wBcxDkYBs2rtP7hmkmZ8RJn/view?usp=sharing" target="_blank">Build</a>

RESUMEN
----------
<a href="https://drive.google.com/file/d/1Gd6IE9nrVOpr-S7TkUCJnWHrVI3rEztR/view?usp=sharing" target="_blank">Enunciado de la práctica</a>.

El objetivo del prototipo es diseñar una inteligencia artificial multicapa que sea capaz de jugar
un partido de fútbol de forma lógica y correcta. Para ello, hemos usado toma de decisiones para la
IA multicapa y navegación para los jugadores. Nos centramos en que los jugadores supieran
atacar y defender correctamente, no en que supiran tirar a puerta o parar el balón de manera realista.
Esto es debido a que la dirección óptima para el portero es hacia donde esté el balón y si
siempre va hacia allí el partido transcurrirá en empate perpetuo, por lo que tanto la dirección de
tiro del delantero como la de parada del portero serán aleatorias.

PLANTEAMIENTO DEL PROYECTO
-----------------------------
El prototipo consiste en un campo de fútbol con 11 jugadores en
cada equipo, dos porterías y muros invisibles que hacen rebotar el balón. Cada vez que un
jugador toca el balón, éste pasa a ser llevado por el jugador que lo toca, evitando así
tener que implementar robos de balón más complejos. Se han utilizado los plugin de Behaviour
Designer y Bolt.

Los jugadores se organizan en diferentes roles, siendo estos delanteros, centrocampistas y defensas.
Los delanteros chutan hacia la portería cuando están a una determinada distancia de ella y tienen hueco
para hacerlo, y lo hacen hacia una dirección aleatoria de la portería para que no sea gol asegurado.

Cuando los jugadores tienen el balón, son dirigidos por un árbol de comportamiento, distinto para cada
rol en el equipo. Cuando no tienen el balón, son dirigidos por una "mente colmena" implementada como máquina de estados, que
toma decisiones dependiendo de la situación del juego dando así como resultado un comportamiento de equipo.

Además, hemos hehco que el tamaño del equipo y sus posiciones límite se puedan cambiar en el editor y la IA siga funcionando y
hemos conseguido que no sea excesivamente determinista, ya que anque hay casos en los que las jugadas iniciales se repiten
por partir de la misma posición, un mímimo cambio altera el resto del partido.

* Escenario: 
![Escenario](Doc/Escenario.PNG?raw=true)

Comportamiento con el balón:

Delanteros
------------
Los delanteros son los encargados de marcar goles, y como tal, tiran a portería siempre que les sea posible (por distancia y hueco).
Si no pueden tirar, avanzan hacia la portería mientras que ningún jugador contrincante se interponga en su camino.
Si además de no poder tirar, no pueden avanzar porque hay un contrincante en mitad del camino, intentan pasar el balón al
jugador más adelantado. En caso de tampoco poder pasarla, sin nada que perder chutan a portería.

* Árbol de decisiones del delantero: 
![Árbol de decisiones del delantero](Doc/Delantero.PNG?raw=true)

Centrocampistas
----------------
Los centrocampistas son los encargados de subir el balón, por lo tanto, su comportamiento es parecido al de los delanteros.
Avanzan con el balón hasta que se ven presionados por un contrincante que se interponga en su camino, en ese momento,
pasan el balón. Si avanzando con el balón llegan hasta una zona (su límite de ataque), intentan pasar el balón al delantero
en mejor posición de tiro y en caso de no poder pasársela a un delantero la pasan al jugador más adelantado posible y libre.

* Árbol de decisiones del centrocampista: 
![Árbol de decisiones del centrocampista](Doc/Centro.PNG?raw=true)

Defensas
------------
Los defensas, como tienen un mayor valor en defensa, cuando reciben el balón intentan pasárselo al jugador que se encuentre
más adelantado y libre.

* Árbol de decisiones del defensa:

![Árbol de decisiones del defensa](Doc/Defensa.PNG?raw=true)

COMPORTAMIENTO DE EQUIPO
----------------------------
* Máquina de estados de la "mente colmena": 
![Máquina de estados](Doc/MenteColmena.PNG?raw=true)

La mente colmena diferencia entre tres estados distintos: ataque, defensa y sin balón.

Durante el ataque, la estrategia consiste en colocar a los defensas de manera que estén preparados para defender y repartidos
por el campo. La mitad de los centrocampistas hacen lo mismo, aunque colocándose en una posición algo más adelantada que los defensas.
Por otra parte, el resto de los centrocampistas avanzan hacia su límite de ataque desmarcándose cuando entre el jugador con el balón
y ellos se encuentra un contrincante. Los delanteros siguen este mismo comportamiento pero avanzando hasta una zona más cercana a
la portería contrincante.

Para la defensa, se sigue un comportamiento parecido al del ataque pero de manera inversa. Los delanteros y la mitad de los
centrocampistas se sitúan preparándose para el ataque igual que hacen los defensas y la mitad de los centrocampistas en ataque solo que
en posiciones distintas. En cuanto al resto de los centrocampistas y los defensas, el jugador más cercano al balón va hacia él intentando
robárselo al contrincante que lo tiene en posesión y el resto cubren cada uno al jugador más cercano que no haya sido ya cubierto.
Para esto, se sitúan entre el jugador a cubrir y el jugador con el balón disminuyendo sus opciones de pase.

En un principio pensamos en una estrategia más sólida, consistente en que cada miembro del equipo cubriera a un jugador rival para evitar que 
el contrincante con el balón pasara, pero esta defensa resultó ser demasiado buena, ya que el partido avanzaba pero ningún equipo lograba marcar
gol nunca.

Objetivos desarrollados
-----------------------------------------------------
* La existencia de un simulador de fútbol sencillo, sin tener en cuenta las infracciones
	propias del fútbol (falta, fuera de juego, bandas). Habrá muros en los extremos del campo
	para que el balón rebote.

* Cada jugador ha de ser capaz de tomar una decisión tanto cuando se encuentre con el
	balón, (continuar con él, la ruta a seguir en ese caso, pasarlo, tirar, etc) como cuando no
	lo tenga.
	
* Que el equipo esté coordinado y exista una estrategia definida tanto para ataque como
	para defensa.

* Que se muestre la puntuación de ambos equipos.

Objetivos adicionales
--------------------------
* Se muestran métricas sobre el comportamiento de la IA en ejecución.
	Se muestran los Raycast que hacen los jugadores y su objetivo de desplazamiento.
	Se muestra el estado en el que se encuentra la "mente colmena".

Pruebas:
---------

<a href="https://drive.google.com/file/d/1oiTF_l14ac8wuzv93agGZgy0WKW4Ba3j/view?usp=sharing" target="_blank">Video Demostración de un partido normal</a>.

<a href="https://drive.google.com/file/d/1Q-BoiNxMI_vyfJaUR3EQ4r7FQ6x4314W/view?usp=sharing" target="_blank">Video Demostración de un partido con algunas posiciones cambiadas</a>.

<a href="https://drive.google.com/file/d/1eMcdngAXUay9nGucBqrNud3gpW-DyoSU/view?usp=sharing" target="_blank">Video de Demostración del estado de ataque</a>.

Para probar el estado de ataque basta con ponerle la velocidad a 0 a uno de los equipos.

<a href="https://drive.google.com/file/d/1xPyljiYnc95vdLB_nZjStLybQi-WDjTr/view?usp=sharing" target="_blank">Video de Demostración del estado de defensa</a>.

Para probar el estado de defensa basta con desactivar los centrocampistas (y hacerlos hijos de un objeto empty fuera de su equipo) y la mente colmena de uno de los equipos y al resto ponerle velocidad 0. Luego se le da el balón a ese equipo.

<a href="https://drive.google.com/file/d/1SEoqC9dBmbyB6MdUaajQiUH7dEM3nk5R/view?usp=sharing" target="_blank">Video Demostración del desmarque</a>.

Para probar el desmarque hay que desactivar gran parte de los jugadores de un equipo (y hacerlos hijos de un objeto empty fuera de su equipo) y su mente colmena. Posteriormente desactivar el comportamiento del delantero del equipo contrario y comentar el método de defensa de la máquina de estados. A partir de ahí se puede colocar un jugador del primer equipo entre el jugador con el balón y uno de sus compañeros y se verá como este último trata de desmarcarse.


En caso de error:
-----------------
Dirigirse a la carpeta OnError del repositorio. Allí se encuentran imágenes de qué objetos y valores hay que asignar a cada script.


Contenido de terceros:
----------------------
El contenido de terceros usado en esta práctica se limita a la textura del campo de fútbol, imagen obtenida mediante Google Images y el uso de colliders en forma de cono, recurso obtenido de la Asset Store de Unity.
