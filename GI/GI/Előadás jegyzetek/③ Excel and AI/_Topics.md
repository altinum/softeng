
={1,2;3,4}
LAMBDA  
LET  
XLOOKUP  
HSTACK  
INDEX  
CHOOSECOLS, CHOOSEROWS  
SORTBY  
UNIQUE  
FILTER

## Adatok beolvasása Power Query-vel



## Táblából Spill Array

**1. A betöltött táblázat (Table) nevének megadása**

Ha a Power Query eredményt táblázatként töltötted be:

1. Kattints bármelyik cellájába a betöltött táblázatnak.
2. A menüszalagon megjelenik a **Táblázateszközök → Tervezés** fül (angolul: _Table Design_).
3. Bal oldalon a **Táblázat neve** mezőbe írd be az új nevet, pl. `tblAdat`, és nyomj Entert.

A nevet szóköz nélkül add meg (betűvel kezdődjön, aláhúzás használható). Ezután a `=tblAdat` képlet az egész táblát kiteríti, és frissítéskor automatikusan igazodik: ha több lett a sor, a spill is hosszabb lesz, ha kevesebb, rövidebb.

Ha csak az adatsorokat akarod, fejléc nélkül, akkor `=tblAdat[#Adatok]` (angolul `tblAdat[#Data]`). A fejléccel együtt: `=tblAdat[#Összes]` (`tblAdat[#All]`).


## Függvények

= SEQUENCE()
= {1,2;3,4} Azért fontos, mert generált Excel táblánál lehet egyetlen string a cellában

```
=categories[#all]
=categories[category_name]
```

```
=CHOOSECOLS(A1#,2)
=CHOOSECOLS(A1#,2,3)
=INDEX(A1#,,1)
```

```
=LET(
ár, CHOOSECOLS(A1,6))
ár * 1.27
```

```
LAMBDA
```


**NVidia H100**



**RTX 5090 (32GB GDDR7 VRAM)**

- Much faster raw compute and memory bandwidth (~1.8 TB/s vs Strix Halo's ~256 GB/s)
- Best choice if your models fit in 32GB — small/medium LLMs (up to ~30B dense, or larger MoE models), image/video generation (Stable Diffusion, Flux), and any fine-tuning work
- Mature CUDA ecosystem — virtually everything just works
- Hits a hard wall once a model needs more than 32GB — you're stuck, unless you add a second GPU
- Costs more and draws way more power (~575W) — needs a real gaming-tower-class PC

**AMD APU with lots of RAM (e.g. Ryzen AI Max+ 395 "Strix Halo")**

- Unified memory architecture lets you allocate up to ~96-112GB to the GPU — so it can _load_ models the 5090 simply cannot fit, including 70B dense models and 100B+ MoE models
- But that memory is much slower (~256 GB/s bandwidth vs the 5090's ~1.8 TB/s), so it's meaningfully slower token generation on models both can run
- Real-world numbers: small/medium models run fine (25-50 tok/s), MoE models are a sweet spot (Qwen 30B-A3B hits ~86 tok/s since it only activates ~3B params per token), but dense 70B models crawl at only 4-12 tok/s
- Comes in a small, efficient mini-PC form factor, uses way less power, and is often cheaper than a high-end 5090 build once you count the PC around it — though the APUs with 128GB configs still run $2,500-4,000
- Software is less mature (ROCm vs CUDA) — more friction, though llama.cpp support has gotten quite good





