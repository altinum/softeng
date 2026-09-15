## Naming conventions

As a first step we need to decide how to name DB objects:

| Naming Convention                    | Example            | Description                                                                    |
| ------------------------------------ | ------------------ | ------------------------------------------------------------------------------ |
| Camel Case                           | `myVariableName`   | Starts with a lowercase letter; each new word starts uppercase.                |
| Pascal Case<br />aka. UpperCamelCase | `MyVariableName`   | Each word starts with an uppercase letter, no spaces or underscores.           |
| Snake Case                           | `my_variable_name` | Words are all lowercase, separated by underscores.                             |
| Kebab Case                           | `my-variable-name` | Words are all lowercase, separated by hyphens (usually in URLs).               |
| Upper Snake Case                     | `MY_VARIABLE_NAME` | Words are all uppercase, separated by underscores.                             |
| (Dot Notation)                       | `my.variable.name` | Words are lowercase and separated by dots (often used in config files).        |
| (Hungarian Notation)                 | `strMyVariable`    | Prefix represents the type (e.g., `str` for string, `int` for integer).        |
| (Train Case)                         | `My-Variable-Name` | Each word starts with an uppercase letter, separated by hyphens (less common). |
| Screaming Kebab Case                 | `MY-VARIABLE-NAME` | All uppercase, words separated by hyphens (rarely used).                       |

We pick `Upper Snake Case` for table names and `Pascal Case` for the field names.

