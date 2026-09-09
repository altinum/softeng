
### Goodhart-törvény

> _"Ha egy mérőszám céllá válik, megszűnik jó mérőszám lenni."_

 Don't hate the players, hate the game.


### A probléma

A lebegőpontos számok nem asszociatívak:

```
(a + b) + c ≠ a + (b + c)
```

Konkrétan:

python

```python
(0.1 + 0.2) + 0.3  →  0.6000000000000001
0.1 + (0.2 + 0.3)  →  0.6
```

---

### Miért fontos ez GPU-nál?

A GPU **párhuzamosan** számol — több ezer mag egyszerre.

```
Token valószínűségek összeadása:
[0.12, 0.34, 0.08, 0.21, ...]  → több millió érték

Sorrend attól függ:
- hány mag áll éppen rendelkezésre
- hogyan osztja szét a scheduler
- memória-hozzáférési sorrend
```


