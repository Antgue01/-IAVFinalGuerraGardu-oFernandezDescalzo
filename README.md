# IAV Final
# Curso 2020 / 2021 - Universidad Complutense de Madrid
# Facultad de Informática - Videojuegos

 Miembros:
  * Nicolás Fernández Descalzo
  * Antonio Jesús Guerra Garduño

<a href="https://drive.google.com/file/d/1X70KL6xWuvTQK7ncNaPO6PiV_ATCf-M4/view?usp=sharing" target="_blank">Video Demo</a>

<a href="https://drive.google.com/file/d/1YKqnutRc1wBcxDkYBs2rtP7hmkmZ8RJn/view?usp=sharing" target="_blank">Build</a>

RESUMEN
----------
<a href="https://drive.google.com/file/d/1Gd6IE9nrVOpr-S7TkUCJnWHrVI3rEztR/view?usp=sharing" target="_blank">Enunciado de la práctica</a>.

El objetivo del prototipo es diseñar una inteligencia artificial multicapa que sea capaz de jugar
un partido de fútbol de forma lógica y correcta. Usaremos toma de decisiones para la
IA multicapa y navegación para los jugadores. Nos centraremos en que los jugadores sepan
atacar y defender correctamente, no en que sepan tirar a puerta o parar el balón correctamente.
Esto es debido a que la dirección óptima para el portero es hacia donde esté el balón y si
siempre va hacia allí el partido transcurrirá en empate perpetuo, por lo que tanto la dirección de
tiro del delantero como la de parada del portero serán aleatorias.

PLANTEAMIENTO DEL PROYECTO
-----------------------------
El prototipo consiste en un campo de fútbol con 11 jugadores en
cada equipo, dos porterías y muros invisibles que hacen rebotar el balón. Cada vez que un
jugador toca el balón, éste pasa a ser llevado por el jugador que lo toca, evitando así
tener que implementar robos de balón más complejos. Se utilizará el plugin de Behaviour
Designer y opcionalmente Bolt.

Los jugadores se organizan en diferentes roles, siendo estos delanteros, centrocampistas y defensas.
Los delanteros chutan hacia la portería cuando estén a una determinada distancia de ella y tienen hueco
para hacerlo, y lo hacen hacia una dirección aleatoria de la portería para que no sea gol asegurado.

Cuando los jugadores tienen el balón, son dirigidos por un arbol de comportamiento, distinto para cada
rol en el equipo. Cuando no tienen el balón, son dirigidos por una "mente colmena" implementada como máquina de estados
toma decisiones dependiendo de la situación del juego implementando así un comportamiento de equipo.

Además, hemos hehco que el tamaño del equipo y sus posiciones límite se puedan cambiar en el editor y la IA funcione y
hemos conseguido que no sea excesivamente determinista, ya que anque hay casos en los que las jugadas iniciales se repiten
por partir de la misma posición, un mímimo cambio altera el resto del partido.

* Escenario: 
![Escenario](Doc/Escenario.PNG?raw=true)

Comportamiento con el balón:

Delanteros
------------
Los delanteros son los encargados de marcar goles, y como tal, tiran a portería siempre que les sea posible (por distancia y hueco).
Si no pueden tirar, avanza hacia la portería mientras que ningún jugador contrincante se interponga en su camino.
Si además de no poder tirar, no puede avanzar porque hay un contrincante en mitad del camino, intenta pasar el balón al
jugador más adelantado. En caso de tampoco poder pasarla, sin nada que perder chuta a portería.

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
la porte´ria contrincante.

Para la defensa, se sigue un comportamiento parecido al del ataque pero de manera inversa. Los delanteros y la mitad de los
centrocampistas se sitúan preparándose para el ataque igal que hacen los defensas y la mitad de los centrocampistas en ataque solo que
en posiciones distintas. En cuanto al resto de los centrocampistas y los defensas, el jugador más cercano al balón va hacia él intentando
robárselo al contrincante que lo tiene en posesión y el resto cubren cada uno al jugador más cercano que no haya sido ya cubierto.
Para esto, se sitúan entre el jugador a cubrir y el jugador con el balón disminuyendo sus opciones de pase.

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
	Se muestran los raycast que hacen los jugadores y su objetivo de desplazamiento.
	se muestra el estado en el que se encuentra la "mente colmena".

Pruebas:
---------

<a href="https://drive.google.com/file/d/1Iuv8a-aHQoyZLW2ZXfkF0rSYrFYvy-zf/view?usp=sharing" target="_blank">Video de Demostración de estados de la cantante</a>.

<a href="https://drive.google.com/file/d/1WEVxThvS_K1Rm5GgykuB6juMDTX1aPVP/view?usp=sharing" target="_blank">Video de Demostración del ciclo de raptar</a>.

<a href="https://drive.google.com/file/d/1XuETTcF7F6TRJnxm8OJdZZXMm6EiG6ay/view?usp=sharing" target="_blank">Video Demostración de interacciones entre el fantasma y el vizconde</a>.

<a href="https://drive.google.com/file/d/1Ty3xN34Pg6Nj1pi6p-_BtXKXKAL1Iuz2/view?usp=sharing" target="_blank">Video Demostración de las luces y el fantasma</a>.
