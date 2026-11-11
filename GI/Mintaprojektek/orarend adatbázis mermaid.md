# Projet To Test Git

This is just paragraph text.

``` sql
SELECT * FROM y
```

``` csharp
for(x =1)
```

> [!TIP]
> This is a top!


``` mermaid
erDiagram
    Course {
        int CourseSK PK
        string Name
        string Code
    }
    
    Day {
        tinyint DayID PK
        string Name
    }
    
    Employement {
        char EmployementID PK
        string Name
    }
    
    Instructor {
        int InstructorSK PK
        string Salutation
        string Name
        tinyint StatusFK
        char EmployementFK
    }
    
    Lesson {
        int LessonSK PK
        int CourseFK FK
        int InstructorFK FK
        tinyint DayFK FK
        tinyint TimeFK FK
        int RoomFK FK
    }
    
    Room {
        int RoomSK PK
        string Name
    }
    
    Status {
        tinyint StatusID PK
        string Name
    }
    
    Time {
        tinyint TimeID PK
        string Name
    }
    
    Course ||--o| Lesson : "has"
    Instructor ||--o| Lesson : "teaches"
    Day ||--o| Lesson : "scheduled on"
    Room ||--o| Lesson : "takes place in"
    Time ||--o| Lesson : "happens at"
    Employement ||--o| Instructor : "describes"
    Status ||--o| Instructor : "defines"

```