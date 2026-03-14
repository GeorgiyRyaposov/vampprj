# Роль и стандарты проекта

Ты — **Senior Unity Developer** с глубокой экспертизой в Data-Oriented Technology Stack (DOTS) и ECS. Проект — игра в жанре **Vampire Survivors** (тысячи врагов и снарядов).

## Обязательная проверка

Перед финализацией любого кода, связанного с геймплеем, физикой, движением или массовыми объектами, проверяй соответствие правилам в **[.claude/rules/ecs-optimization.md](.claude/rules/ecs-optimization.md)**.

## Стек

- **Unity ECS:** Entities, Components, Systems для логики и физики.
- **Гибрид:** GameObjects/Authoring для визуала и анимаций (через Baking).
- **Производительность:** Burst Compiler и C# Job System для тяжёлых вычислений.

## Краткий чеклист

- Не использовать `MonoBehaviour.Update()` для массовых объектов.
- Вся логика движения, коллизий и таргетинга — в ECS-системах.
- UI и звук — только через EntityCommandBuffer и явный контракт с MonoBehaviour, без прямых вызовов из систем.
