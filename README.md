# Galcon Test Task

Версия движка: *Unity 2019.3.6f1*.

Используемые доп.пакеты: *Cinemachine*, *NavMesh*, *TextMeshPro*, *Zenject*.

Дополнительные ассеты: *SpriteOutline*.

Стартовая сцена: *Assets/_Game/Scenes/Game.unity*.


Далее все пути будут указаны относительно директории *Assets/_Game*.


### Замечания:

- В проекте для скорости разработки использовались модули и компоненты из моих ранних работ, которые возможно не везде имеют заточенную под данный контекст реализацию. Но и костыльными они не являются.
- У меня закрались сомнения насчёт понимания требования "планеты расположены друг от друга на расстоянии не менее чем сумма радиусов соседних планет", а точнее в том, что считать соседними планетами, и я решил не тратить время на выяснения. Сейчас отдельным классом происходит случайная генерация в заданной области с возможностью задать минимальное расстояние между поверхностями соседних планет. Если что-то где-то не так, то в Di-контейнере можно просто поменять реализацию класса на другую более подходящую (которой пока нет).
- Честно говоря, NavMesh работает отвратительно, я достаточно долго пытался привести его в чувство, но пока выглядит так, как выглядит. В **ShipParameters** можно использовать **SpeedConfig** без NavMesh'а. Тогда корабли будут двигаться засчёт других модулей, но не будут облетать планеты.
- В проекте очень активно используется событийная модель на **UnityEvent** и **UnityAction**. Все подписки, которые обеспечивают работу логики, я постарался произвести внутри кода, оставив поля в инспекторе для организации работы с UI и прочими визуальными штуками, которые, в частности, может править и гейм-дизайнер. Вообще в целом в инспекторе постарался скрыть все поля, которые не понадобятся дизайнеру.
- Планировал успеть *UI* и хотя бы элементарные кнопки "старт", "выход", "пауза", но в задании на это не было акцентировано внимание, поэтому надеюсь, что это не сильно испортит впечатление.
- Все возможные настройки вынесены в папку */Parameters* в виде **ScriptableObject'ов**. Я не успел выполнить все дополнительные пункты и вообще как-то завершить игру, но я успел дать немного вольности в возможности дизайнерам настраивать всякие параметры.
- Из кастомных редакторов успел сделать на скорую руку один **PropertyDrawer** и один **CustomEditor**. Оба лежат в папочке *Scripts/Editor*.


### Правила именований

**Pascal casing:** Определения классов, структур, интерфейсов, перечислений, значения перечислений, методы, пространства имён, сериализованные поля (в т.ч. SerializeField).

**Camel casing:** Локальные переменные, аргументы методов, поля и свойства.

**Caps:** Константы.

**Префикс _:** Private/Protected поля и свойства.


#### Примеры:

[SerializeField] private T _Value;

[SerializeField] public T Value;

private T _value;

protected T _value { get; set; }

internal T value;

public T value { get; }

private const T _VALUE;

internal const T VALUE;


public void Method();

private Tout Method(T1 a1, T2 a2, ... );


class Example;

interface IExample;

internal enum Example { Example1, Example2, ... }


### Что на сцене

Для определения отображаемой зоны исользуется **TargetGroup** виртуальной камеры **Cinemachine**, в которой закреплены **ViewBorders** для каждой стороны экрана. Весь геймплей происходит в рамках заданной области.

В объекте **NavMesh** задана плоскость, по которой перемещаются корабли. Именно на этой плоскости происходит запекание карты передвижений после каждой генерации планет.

Внутри данного объекта так же находятся **Level**, в котором существует и работает весь игровой уровень, и **Pool** - контейнер для объектов, хранящихся в пуле объектов.

**Player** - объект, который отвечает за игрока: за его ввод (**InputManager**), за выделение планет (**SelectionManager**), за действия с планетами (**Player**) и за его идентификацию (**PlanetOwner**).

**PlanetsManager** управляет генерацией планет и доступом к ним.
Соответственно, ещё есть **Ship** и **Planet**.
*Корабли* создаются внутри каждой планеты из общего пула кораблей, которые хранятся в объекте **Pool**.

На каждой планете есть **PlayerOwner**, который определяет, кому принадлежит планета. Каждый игрок имеет свой уникальный тэг.

Список доступных тегов и их настроек находится в *Parameters/PlanetOwnersParameters*. По-хорошему, во все *ScriptableObject'ы*, которые работают с тэгами, нужно подтягивать список доступных тэгов из **PlanetOwnersParameters**, который можно оформить в виде *MaskField*. Но будем считать, что это "*фичи для будущих апдейтов*".

На сцене объект **Player** так же имеет **PlayerOwner**, в котором нужно указать тэг, чьими планетами можно будет управлять.

Для скрытия счётчика на планетах не принадлежащих игроку, использую костыльное решение в виде **Parameters/PlanetPopulationVisibleParameters** и **AutoHiderPopulationView.cs**. В сжатых сроках это показалось самым "элегантным" решением.

## Применяемые паттерны:

Эти заметки я пишу уже после работы, поэтому могу не вспомнить все реализованные кейсы.


- Весь проект использует **IoC/DI принципы**.
- Все игровые объекты на сцене используют некоторое подобие **MVP** архитектуры. У них есть *Model* и *View*, которые не связаны между собой и общаются только своего *Presenter'а*.
- Для генерации планет используется **Фабричный метод**, доступный "из коробки" в рамках *Zenject*.
- Для генерации кораблей используется **Объектный пул**, так же доступный в рамках *Zenject*.
- *PlanetsConfigurator* - это по своей сути реализация **Фасада**.
- *InputHandler* - представитель паттерна **Цепочка обязанностей**.
- *HandlersBuilder* и *HandlersDirector* - реализация паттерна **Строитель**.
- *ShipsProducer* - **Декоратор** для пула (чисто по смысловой нагрузке и начальной идее. Сам *Producer* не реализует ту же абстракцию, что и пул, т.к. мне не хотелось тащить дальше всё то, что предлагает реализовать Zenject, и Zenject не может выдавать интерфейсы, по крайней мере, я такой возможности не увидел. Так что в каком-то роде это больше даже **Мост**).
