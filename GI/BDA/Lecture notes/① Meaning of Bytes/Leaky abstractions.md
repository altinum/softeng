
A **leaky abstraction** is an abstraction that hides the underlying complexity only partially. Eventually, details of the implementation “leak” through the abstraction and affect how we have to use it.

Joel Spolsky famously described this in his 2002 article **“The Law of Leaky Abstractions.”** His central point is that _all non-trivial abstractions, to some degree, are leaky_.

[Joel Spolsky — The Law of Leaky Abstractions](https://www.joelonsoftware.com/2002/11/11/the-law-of-leaky-abstractions/?utm_source=chatgpt.com)

(Joel Spolsky is a software developer, programmer, and writer best known as the co-founder of Stack Overflow and for his influential blog _Joel on Software_. He also created the Visual Basic module for Excel at Microsoft.)
## Car's brake

Consider a **car's brake pedal**. It provides a convenient abstraction: press the pedal and the car slows down. Normally, you don't need to think about friction, brake discs, engine braking, or the forces acting on the car.

But imagine driving **down a steep slope**. If you put the car in **neutral** and rely entirely on the brake pedal, the brakes have to dissipate the car's gravitational energy as heat. They can become extremely hot and lose effectiveness. If you instead select an appropriate gear, **engine braking** can help control the speed and reduce the load on the brakes.

The abstraction—“press the brake to slow the car”—has leaked. To use the system effectively in this situation, you need to understand what is happening underneath.

The same happens in software: an abstraction can hide complexity during normal operation, but unusual conditions, performance problems, or failures often expose the underlying implementation.

### “Excel does the maths”

Excel makes arithmetic feel effortless: you enter a formula, and **Excel does the maths**. However, the abstraction leaks when we encounter the limitations of floating-point numbers. Excel commonly represents numbers using IEEE 754 double-precision floating-point arithmetic, which cannot represent many decimal fractions exactly. For example, `0.1 + 0.2` may internally produce a value slightly different from `0.3`. Excel normally hides these tiny errors by displaying a rounded result, but they can become visible in comparisons, accumulated calculations, or formulas involving many operations. In other words, **Excel does the maths—but the maths is still performed using finite-precision numbers, with all the limitations that entails.**

**Abstractions hide complexity; they do not make the underlying complexity disappear.**