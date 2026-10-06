## Integer types

```mermaid
graph BT
    sbyte["sbyte<br/>-128 .. 127"] --> short["short<br/>-32,768 .. 32,767"]
    byte["byte<br/>0 .. 255"] --> short
    byte --> ushort["ushort<br/>0 .. 65,535"]
    short --> int["int<br/>-2.1B .. 2.1B"]
    ushort --> int
    ushort --> uint["uint<br/>0 .. 4.3B"]
    int --> long["long<br/>±9.2×10^18"]
    uint --> long
    uint --> ulong["ulong<br/>0 .. 1.8×10^19"]
```


A few notes on why it's shaped this way:

- **byte** sits below both **short** and **ushort** — every byte value fits in either, since byte is non-negative and small enough for both.
- **short** and **ushort** both feed into **int** — their ranges are disjoint at the negative end but both fully fit inside int's range.
- **long** and **ulong** are both "tops," but neither contains the other: ulong's max (~1.8×10¹⁹) is far bigger than long's max (~9.2×10¹⁸), but long can go negative and ulong can't — so they _intersect_ (on 0 .. long.MaxValue) without either containing the other.

## Fraction types

``` mermaid
graph BT
    float["float<br/>~7 digits precision<br/>±1.5×10^-45 .. ±3.4×10^38"] --> double["double<br/>~15-17 digits precision<br/>±5.0×10^-324 .. ±1.7×10^308"]
    decimal["decimal<br/>28-29 digits precision<br/>±1.0×10^-28 .. ±7.9×10^28"]
```
## The BIG picture

``` mermaid
graph BT
    sbyte["sbyte"] --> short["short"]
    byte["byte"] --> short
    byte --> ushort["ushort"]
    short --> int["int"]
    ushort --> int
    ushort --> uint["uint"]
    int --> long["long"]
    uint --> long
    uint --> ulong["ulong"]

    long -- "implicit, exact" --> decimal["decimal"]
    ulong -- "implicit, exact" --> decimal

    long --> BigInteger["BigInteger"]
    ulong --> BigInteger
```

	