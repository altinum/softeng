
https://bkk.hu/bkk-partnerek/egyeb/gtfs-menetrendi-adatok-programozoknak/

Inné töltsétek le, mielőtt kitiltják az egyetem IP tartományát:
![[budapest_gtfs.zip]]

https://opendata.bkk.hu/data-sources


``` mermaid
erDiagram
    AGENCY {
        VARCHAR agency_id PK
        VARCHAR agency_name
        VARCHAR agency_url
        VARCHAR agency_timezone
        VARCHAR agency_lang
        VARCHAR agency_phone
        VARCHAR agency_fare_url
    }

    STOP {
        VARCHAR stop_id PK
        VARCHAR stop_code
        VARCHAR stop_name
        VARCHAR stop_desc
        DECIMAL stop_lat
        DECIMAL stop_lon
        VARCHAR zone_id
        VARCHAR parent_station FK
        TINYINT location_type
        TINYINT wheelchair_boarding
    }

    ROUTE {
        VARCHAR route_id PK
        VARCHAR agency_id FK
        VARCHAR route_short_name
        VARCHAR route_long_name
        VARCHAR route_desc
        TINYINT route_type
        VARCHAR route_url
        VARCHAR route_color
        VARCHAR route_text_color
    }

    TRIP {
        VARCHAR trip_id PK
        VARCHAR route_id FK
        VARCHAR service_id FK
        VARCHAR trip_headsign
        VARCHAR trip_short_name
        TINYINT direction_id
        VARCHAR block_id
        VARCHAR shape_id FK
        TINYINT wheelchair_accessible
        TINYINT bikes_allowed
    }

    STOP_TIME {
        VARCHAR trip_id PK, FK
        INTEGER stop_sequence PK
        VARCHAR stop_id FK
        VARCHAR arrival_time
        VARCHAR departure_time
        VARCHAR stop_headsign
        TINYINT pickup_type
        TINYINT drop_off_type
        DECIMAL shape_dist_traveled
        TINYINT timepoint
    }

    CALENDAR {
        VARCHAR service_id PK
        DATE start_date
        DATE end_date
        BOOLEAN monday
        BOOLEAN tuesday
        BOOLEAN wednesday
        BOOLEAN thursday
        BOOLEAN friday
        BOOLEAN saturday
        BOOLEAN sunday
    }

    CALENDAR_DATE {
        VARCHAR service_id PK, FK
        DATE date PK
        TINYINT exception_type
    }

    SHAPE {
        VARCHAR shape_id PK
        DECIMAL shape_pt_lat
        DECIMAL shape_pt_lon
        INTEGER shape_pt_sequence PK
        DECIMAL shape_dist_traveled
    }

    FREQUENCY {
        VARCHAR trip_id PK, FK
        VARCHAR start_time PK
        VARCHAR end_time
        INTEGER headway_secs
        TINYINT exact_times
    }

    TRANSFER {
        VARCHAR from_stop_id PK, FK
        VARCHAR to_stop_id PK, FK
        TINYINT transfer_type
        INTEGER min_transfer_time
    }

    AGENCY |o--o{ ROUTE : operates
    ROUTE ||--o{ TRIP : contains
    TRIP ||--o{ STOP_TIME : has
    STOP ||--o{ STOP_TIME : serves

    CALENDAR |o--o{ TRIP : defines_service
    CALENDAR |o--o{ CALENDAR_DATE : exceptions

    SHAPE |o--o{ TRIP : follows

    TRIP ||--o{ FREQUENCY : frequency

    STOP ||--o{ TRANSFER : from
    STOP ||--o{ TRANSFER : to_stop

    STOP |o--o{ STOP : parent
```


```
erDiagram
    AGENCY {
        VARCHAR agency_id PK
        VARCHAR agency_name
        VARCHAR agency_url
        VARCHAR agency_timezone
        VARCHAR agency_lang
        VARCHAR agency_phone
        VARCHAR agency_fare_url
    }

    STOP {
        VARCHAR stop_id PK
        VARCHAR stop_code
        VARCHAR stop_name
        VARCHAR stop_desc
        DECIMAL stop_lat
        DECIMAL stop_lon
        VARCHAR zone_id
        VARCHAR parent_station FK
        TINYINT location_type
        TINYINT wheelchair_boarding
    }

    ROUTE {
        VARCHAR route_id PK
        VARCHAR agency_id FK
        VARCHAR route_short_name
        VARCHAR route_long_name
        VARCHAR route_desc
        TINYINT route_type
        VARCHAR route_url
        VARCHAR route_color
        VARCHAR route_text_color
    }

    TRIP {
        VARCHAR trip_id PK
        VARCHAR route_id FK
        VARCHAR service_id FK
        VARCHAR trip_headsign
        VARCHAR trip_short_name
        TINYINT direction_id
        VARCHAR block_id
        VARCHAR shape_id FK
        TINYINT wheelchair_accessible
        TINYINT bikes_allowed
    }

    STOP_TIME {
        VARCHAR trip_id PK, FK
        INTEGER stop_sequence PK
        VARCHAR stop_id FK
        VARCHAR arrival_time
        VARCHAR departure_time
        VARCHAR stop_headsign
        TINYINT pickup_type
        TINYINT drop_off_type
        DECIMAL shape_dist_traveled
        TINYINT timepoint
    }

    CALENDAR {
        VARCHAR service_id PK
        DATE start_date
        DATE end_date
        BOOLEAN monday
        BOOLEAN tuesday
        BOOLEAN wednesday
        BOOLEAN thursday
        BOOLEAN friday
        BOOLEAN saturday
        BOOLEAN sunday
    }

    CALENDAR_DATE {
        VARCHAR service_id PK, FK
        DATE date PK
        TINYINT exception_type
    }

    SHAPE {
        VARCHAR shape_id PK
        DECIMAL shape_pt_lat
        DECIMAL shape_pt_lon
        INTEGER shape_pt_sequence PK
        DECIMAL shape_dist_traveled
    }

    FREQUENCY {
        VARCHAR trip_id PK, FK
        VARCHAR start_time PK
        VARCHAR end_time
        INTEGER headway_secs
        TINYINT exact_times
    }

    TRANSFER {
        VARCHAR from_stop_id PK, FK
        VARCHAR to_stop_id PK, FK
        TINYINT transfer_type
        INTEGER min_transfer_time
    }

    AGENCY |o--o{ ROUTE : operates
    ROUTE ||--o{ TRIP : contains
    TRIP ||--o{ STOP_TIME : has
    STOP ||--o{ STOP_TIME : serves

    CALENDAR |o--o{ TRIP : defines_service
    CALENDAR |o--o{ CALENDAR_DATE : exceptions

    SHAPE |o--o{ TRIP : follows

    TRIP ||--o{ FREQUENCY : frequency

    STOP ||--o{ TRANSFER : from
    STOP ||--o{ TRANSFER : to_stop

    STOP |o--o{ STOP : parent
```





