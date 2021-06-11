# IAV Final GuerraGarduñoFernandezDescalzo
# Curso 2020 / 2021 - Universidad Complutense de Madrid
# Facultad de Informática - Videojuegos

 Miembros:

  * David Czepiel
  * Pablo Villapún Martín
  * Nicolás Fernández Descalzo
  * William Molina Cumba
  * Marco Iván Merino Hernández


# PRACTICA 3
<a href="https://drive.google.com/file/d/1X70KL6xWuvTQK7ncNaPO6PiV_ATCf-M4/view?usp=sharing" target="_blank">Video Demo</a>

Resumen
-----------------------------
<a href="https://drive.google.com/file/d/1Gd6IE9nrVOpr-S7TkUCJnWHrVI3rEztR/view?usp=sharing" target="_blank">Enunciado de la práctica</a>.

En resúmen, el prototipo que se desarrolla se centra reproducir la toma de decisiones y los comportamientos de varios agentes inteligentes, la creación de un avatar para el jugador y un escenario en Unity en el que poder reproducir lo solicitado en la práctica.

Para la práctica se debe usar las herramientas Bolt y Behavior Designer.

Previo al desarrollo se ha realizado un estudio complejo para poder desmembrar lo que cada agente inteligente debe de hacer, los diferentes comportamientos, la toma de decisiones, etc. Esta información se muestra a continuación:

ESTANCIAS DEL ENTORNO VIRTUAL
-----------------------------

Escenario (E): 
* Estancia inicial de la cantante.
* Conecta con el patio de butacas y los palcos.
* Contiguo a las Bambalinas. 
* Tiene una trampilla que llega al sótano oeste, unidireccional E-> SO.

Bambalinas (B): 
* Conecta con el escenario y sótano oeste.
* Tiene una rampa que llega al sótano este, unidireccional B-> SE.

Patio de butacas (P): 
* Estancia inicial del público.
* Conectado con el escenario.
* Es visible desde los palco este y oeste.
* Dispone de dos lámparas que pueden ser tiradas o reparadas.

Palco oeste (PO): 
* Estancia inicial del vizconde.
* Conecta con escenario y sótano oeste.
* Permite ver el patio de butacas (pero la altura impide que no sea al revés).
* Tiene una palanca que deja caer la lámpara oeste del patio de butacas.
  
Palco Este (PE):
* Estancia similar al palco oeste.
* Conecta con el escenario y sótano este.
* Permite ver el patio de butacas (pero la altura impide que no sea al revés).
* Tiene una palanca que deja caer la lámpara este del patio de butacas.

Sótano oeste (SO):
* Conecta con el palco oeste y bambalinas.
* Conecta con el sótano norte pero hace falta ir en barca.

Sótano este(SE):
* Conecta el palco este
* Conecta con sótano norte y con la sala de música pero hace falta ir en barca.

Sótano norte (SN):
* Conecta con la celda.
* Conecta con la sala de música, con el sótano este y el sótano oeste pero hace falta ir en barca.

Celda (C):
* Conecta con el sótano norte.
* Tiene una palanca que abre y cierra la puerta de la celda.
* Estancia donde el fantasma deja a la cantante para completar su secuestro.

Sala de música (SM):
* Estancia inicial del fantasma.
* Conecta con sótano este y con sótano norte pero hace falta ir en barca.


ERIK - EL FANTASMA (AGENTE INTELIGENTE)
---------------------------------------
Villano, antihéroe, músico deforme que vive escondido en los subterráneos del Palacio Garnier, la casa de la Ópera.

Su objetivo es secuestrar a la cantante, para ello intentará buscarla en las bambalinas y el escenario prioritariamente y si no, explorando otras estancias aleatoriamente.

Se puede desplazar usando las barcas.

No pisará el escenario mientras haya público. Puede derribar lámparas para ahuyentar a los espectadores.

Si se cuestra a la cantante intentará llevarla a la celda secreta (intentando usar el camino con menor coste, recordará para ello las últimas posiciones de las barcas que haya visto) para encerrarla.

Mientras secuestra a la cantante la puede soltar en cualquier punto por voluntad propia.

Mientras secuestra a la cantante soltará forzadamente a la cantante si el Vizconde le golpea.

Si lleva a la cantante a la celda la dejará allí, cerrará la puerta y se irá hasta la Sala de Música donde permanecerá indefinidamente hasta que vuelva a ser alterado.

Si hay público mirando intentará tirar alguna lámpara para espantar a la gente.

Si la cantante fue secuestrada y liberada posteriormente y el fantasma la oye cantar intentará raptarla de nuevo.

Si el vizconde rompe su piano u otros muebles de su guarida y está cerca (tiene que estar en alguna parte del subterráneo), dejará todo lo que esté haciendo e irá corriendo a "reparar" su piano. 


CHRISTINE DAAÉ - CANTANTE (AGENTE INTELIGENTE)
------------------------------------------
Diva, trabaja sobre el escenario. A veces se refugia entre bambalinas porque sufre crisis de ansiedad. 

El fantasma la asusta y no ofrece resistencia si la captura.
Puede ser llevada en brazos por el fantasma o el vizconde.

En el Escenario se pone a cantar.
Suele ir del Escenario a las Bambalinas cada pocos segundos para hacer un descanso.

Si acaba en una estancia que no esté conectada con el escenario o las bambalinas se sentirá confusa y merodeará erráticamente.
Si está confusa, merodeando y ve al vizconde, lo seguirá con la esperanza de retomar su trabajo.
Si llega a al escenario o las Bambalinas volverá a sus quehaceres.


EL PÚBLICO - (AGENTES INTELIGENTES)
----------------------------------------
Son los agentes más sencillos. 

Su funcionalidad es la de estar en el Patio de Butacas para ver la actuación de la cantante.

Si el fantasma tira una lámpara el Patio de Butacas se oscurece y los espectadores huirán al vestíbulo.
El público no volverá hasta que las lámparas no estén bien colocadas y se les haya pasado el disgusto (tiempo de espera).


RAOUL DE CHAGNY - VIZCONDE (AVATAR DEL JUGADOR)
-----------------------------------------------
Joven, atractivo, pretendiente de Christine. La anima y le hace volver a las tablas.

Puede interactuar con lámparas para arreglarlas.
Puede abrir la puerta de la Celda.
Puede coger a la cantante en brazos para llevarla consigo o para dejarla en el suelo.

Puede golpear el piano o los muebles de la Sala de Música para enfadar a el fantasma.
Puede golpear al fantasma, dejándolo unos segundos aturdido y soltará a la cantante si el fantasma la estaba raptando.
Puede golpear muebles que es escucharán en el subterráneo.


Propuesta de la solución
----------------------------
La creación del escenario en se ha realizado en Unity usando los tipos básicos de Game Objects, asociados a una malla de navegación. 

* Nav Mesh: Malla de Navegación: ![Malla de Navegacion](../Doc/EntornoVirtual.NavMesh.jpg?raw=true)

Para el uso de las barcas se utiliza puntos de anclaje en las diferentes seciones de la malla de navegación.
* Mesh Links: Anclajes de Navegación: ![Mesh Links](../Doc/MeshLinks.png?raw=true)


El comportamiento del fantasma se ha reproducido en Behavior Designer, con la asociación de scripts en C# adicionales.
El comportamiento de la cantante y el público se ha reproducido en Bolt usando el modo de programadores.
La usabilidad del Vizconde se ha reproducido vía scripts en C#.

A continuación se muestran los diferentes estudios que se han realizado para concretar los agentes inteligentes:

ERIK - FANTASMA
* Esquema de Árbol de Decisiones inicial: 
![Esquema de Árbol de Decisiones](../Doc/ComportamientoFantasmaInicial.jpg?raw=true)

* Esquema de Árbol de Comportamientos final: 
![Esquema de Árbol de Comportamientos](../Doc/ComportamientoFantasmaFinal.jpg?raw=true)

CHRISTINE DAAÉ - CANTANTE 
* Esquema de Máquina de estados: 
![Maquina Estados](../Doc/MaquinaEstadoCantante.jpg?raw=true)



Objetivos obligatorios desarrollados de la práctica
---------------------------------------------------
* Se utiliza malla de navegación proporcionado por Unity para la navegación de los agentes inteligentes.
* El movimiento del Vizconde puede ser controlado libremente mediante los cursores.
* El Vizconde puede interactuar con los elementos: Cantante, Fantasma, Piano, Lámparas, Celda, Puertas y Barcas.
* Se han añadido varias cámaras, una por cada personaje y otra general en vista cenital.
* El público huye tras la caída de alguna de las lámparas y regresan cuando están reparadas.
* Máquina de estados de la cantante, puede ser atrapada, raptada, cogida en brazos, etc.
* Se ha añadido movimiento, percepción y decisión a la cantante.
* Árbol de comportamiento completo del fantasma que realiza todas las interacciones solicitadas.
* Sistema sensorial para el ruido y visión añadidos en el fantasma.


Ampliaciones añadidas a la práctica
---------------------------------------
* Escenario con geometría compleja.
* Saltos insertados en la malla de navegación.
* Mecanismos complejos: botones para abrir y cerrar pasadizos, puertas, barcas, etc.
* Mejorar el razonamiento del fantasma sobre el estado y posición de los elementos del entorno (memoria).
* Mejorar la gestión sensorial del fantasma para que vea y oiga a los personajes sin compartir estancia y reaccione con estas percepciones.

# SCRIPTS ADICIONALES


Pruebas:
---------
<a href="https://drive.google.com/file/d/1Iuv8a-aHQoyZLW2ZXfkF0rSYrFYvy-zf/view?usp=sharing" target="_blank">Video de Demostración de estados de la cantante</a>.

<a href="https://drive.google.com/file/d/1WEVxThvS_K1Rm5GgykuB6juMDTX1aPVP/view?usp=sharing" target="_blank">Video de Demostración del ciclo de raptar</a>.

<a href="https://drive.google.com/file/d/1XuETTcF7F6TRJnxm8OJdZZXMm6EiG6ay/view?usp=sharing" target="_blank">Video Demostración de interacciones entre el fantasma y el vizconde</a>.

<a href="https://drive.google.com/file/d/1Ty3xN34Pg6Nj1pi6p-_BtXKXKAL1Iuz2/view?usp=sharing" target="_blank">Video Demostración de las luces y el fantasma</a>.

ASSETS Y REFERENCIAS
================================
* AI for Games 3rd Edition (2019) - Ian Millington
* Referencias utilizadas para hacer uso de las diversas herramientas de Unity
* [NavMeshAgents](https://www.youtube.com/watch?v=VJ2iW_laA-Y)
* [Behavior Designer](https://www.youtube.com/watch?v=mPbIx5G8Y1E)
* [Bolt](https://www.youtube.com/watch?v=SVpkh3kMIcg)
	
La solucion ha sido basada en el proyecto proporcionado por Federico Peinado referente a Decisión.
