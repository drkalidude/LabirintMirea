# LabirintMirea
 For Laboratory Mirea


# Описание
**AR Labirint SpeedRun** – это 3D-игра, созданная с использованием Unity.  
Идея проекта заключается в том, чтобы как можно быстрее пройти уровни, преодолевая врагов с помощью физики.  
Проект включает:
- Реалистичную физику движения объектов
- Интерактивное окружение с использованием триггеров и коллайдеров

# Скриншоты проекта
<!-- Пример добавления скриншотов. Убедитесь, что пути к изображениям корректны. -->
![ScreenSot6](https://github.com/user-attachments/assets/367e536a-ee95-4227-b4d7-f595c5e06e75)

# Инструкция по запуску

## Требования
- **Unity:** Версия 2022.3.38f1 LTS или выше (рекомендуется для стабильности проекта)
- **SDK:** Visual Studio 2019/2022 с поддержкой .NET Framework 4.x (для редактирования кода)
- **ОС:** Проект совместим с macOS
- **Xcode:** Необходим для сборки под iOS
- **Xr Plug-in Management**
- **Apple ARKit**
- **XR Interaction Toolkit**
- **AR Foundation**
---

## Руководство по сборке и настройке iOS

### Необходимые условия  
- **Устройство с macOS**: MacBook с macOS Ventura или новее.  
- **Xcode**: Версия 14+ (совместимая с вашей версией Unity).  
- **Unity**: Установка через [Unity Hub](https://unity.com/download) с поддержкой **iOS Build Support**.  
- **Аккаунт разработчика Apple**: Для подписи приложений и тестирования устройств.  

---

### Шаг 1: Настройка среды  
1. **Установите Unity Hub**:  
   - Скачайте с [официального сайта Unity](https://unity.com/download).  
   - Добавьте необходимую версию Unity с поддержкой **iOS Build Support** (через Unity Hub → Installs → Add Modules).  

2. **Установите Xcode**:  
   - Скачайте из [App Store](https://apps.apple.com/us/app/xcode/id497799835) или [Apple Developer](https://developer.apple.com/xcode/).  
   - Откройте Xcode и примите лицензионное соглашение.  

3. **Настройте учетную запись разработчика Apple**:  
   - В Xcode: **Параметры → Учетные записи → Добавить Apple ID**.  

---


### Шаг 2: Настройка проекта Unity

1. **Скачайте проект и запустите**:  
   - Ссылка на проект: https://disk.yandex.ru/d/y00glLvxzlcJhA
2. **Установите нужные пакеты**:  
   - Установите **XR Plugin** для работы с расширенной реальностью.
   - Установите **Apple ARKit** для работы с AR на устройствах iOS.
![screenshot3](https://github.com/user-attachments/assets/6238122f-3663-4436-9a8f-2bf9375252cc)


3. **Настройте проект**:  
   В настройках проекта укажите, для чего будет использоваться камера, и убедитесь, что настройки соответствуют требованиям AR.
![screenshot3](https://github.com/user-attachments/assets/3f3b5804-5a1f-48b9-ba9a-eaa57bbfcee5)

4. **Установите XR Interaction Toolkit**:  
   Также скачайте и установите **AR Starter Assets** для AR-примеров и шаблонов.
![Снимок экрана 2025-03-09 в 20 51 23](https://github.com/user-attachments/assets/200ea263-77ec-499d-99bc-99607fd41bb7)

5. **Соберите проект для iOS**:  
   Перейдите в **File > Build Settings**, выберите **iOS** и нажмите **Build**.
![Снимок экрана 2025-03-09 в 20 51 52](https://github.com/user-attachments/assets/00b1c22b-4f04-4b25-b5ac-d1460cee18a8)

6. **Откройте проект в Xcode**:  
   После сборки откройте проект в **Xcode**, чтобы настроить и собрать его для iOS.
<img width="1389" alt="Снимок экрана 2025-03-09 в 20 52 25" src="https://github.com/user-attachments/assets/9f38a332-1d87-4317-b7e1-dc864aa2074a" />


7. **Настройте профиль**:  
   В Xcode настройте профиль разработчика и убедитесь, что все сертификаты и настройки правильны.
<img width="1128" alt="Снимок экрана 2025-03-09 в 20 52 50" src="https://github.com/user-attachments/assets/d932b780-c3f8-48de-8588-a7b29158336b" />


8. **Подключите iPhone к компьютеру и запустите билд на устройство**:  
   Подключите ваше устройство через USB и выберите его в Xcode для запуска.

9. **Разрешите неизвестному разработчику**:  
   Если устройство не доверяет вашему разработчику, перейдите в **Настройки > Основные > Управление устройством** и разрешите запуск приложения.


# Документация проекта

## Обзор сценариев

### 1. **Bullet**  
- **Назначение**: Управляет поведением пули.  
- Управляет движением, столкновениями и автоуничтожением.  
- Наносит урон врагам/игроку при попадании.  

### 2. **FinishTrigger**  
- **Назначение**: Триггер завершения уровня.  
- Активируется при попадании игрока (например, коллайдера).  
- Триггер вызывает события завершения уровня (следующий уровень, сохранение прогресса).  

### 3. **LevelSelectionUI**  
- **Назначение**: Интерфейс выбора уровня.  
- Отображает уровни.  
- Использует `PlayerPrefs` для отслеживания прогресса.  

### 4. **MovementSphere**  
- **Назначение**: Контроллер сферического движения.  
- Управляет основанным на физике или ручным движением на сферических поверхностях.  
- Обрабатывает ввод игрока (клавиатура, касание).  

### 5. **PlayerHealth**  
- **Назначение**: Управление здоровьем игрока.  
- Отслеживает HP, обрабатывает повреждения и события смерти.  
- Обновляет пользовательский интерфейс здоровья (например, панель HP).  

### 6. **ScanAndSpawn**  
- **Назначение**: Система спавна объектов.  
- Сканирует области в поисках точек спавна (например, `Physics.OverlapSphere`).  
- Порождает врагов/предметы, используя объединение объектов.  

### 7. **TransformController**  
- **Назначение**: Управление трансформацией объектов.  
- Управляет анимацией положения, вращения или масштабирования (например, движущиеся платформы).  

### 8. **TrapShooter**  
- **Назначение**: Система снарядов для ловушек.  
- Стреляет снарядами с интервалом или по триггерам.  
- Настраивает скорость/повреждения снарядов.  


