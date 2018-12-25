# Configuration #

## Contents

* [Export](#export)

---

### Export ###

If you export rules, the rules will be saved inside the config directory of Umbraco in the file: `hideProperties.rules.js`.

You can add the following keys to your appSettings in the web.config.

#### Enable Export ####

You can enable export of rules, if enabled, this will display a export button in the dashbaord. 

```xml
<add key="hideProperties:EnableExport" value="true"/>
```

If the setting isn't defined, the default value will be: `true`.

#### Export on save ####

You can enable export on save, this will save the rules when a rule is created, edited or deleted.

```xml
<add key="hideProperties:ExportOnSave" value="false"/>
```

If the setting isn't defined, the default value will be: `false`. If the setting `Enable Export` is false, this rule does nothing.