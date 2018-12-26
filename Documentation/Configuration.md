# Configuration #

## Contents

* [Export](#export)
* [Import](#import)

---

### Export ###

If you export rules, the rules will be saved inside the config directory of Umbraco in the file: `hideProperties.rules.js`.

You can add the following keys to your appSettings in the web.config.

#### Enable export ####

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

If the setting isn't defined, the default value will be: `false`. If the setting `Enable export` is false, this rule does nothing.

---

### Import ###

If you want import rules, you can export the rules first, or you create a json file inside the config directory of Umbraco, the name of the file must be `hideProperties.rules.js`.

```json
[
  {
    "Key": "d1481b5f-c1e8-4fb6-aecf-4baba032ce04",
    "IsActive": true,
    "ContentTypeAlias": "contentBase",
    "Tabs": "Settings",
    "Properties": "pageTitle",
    "UserGroups": "admin",
    "IsDeleted": false
  },
  ...
]
```

You can add the following keys to your appSettings in the web.config.

#### Enable import ####

You can enable import of rules, if enabled, this will display a import button in the dashbaord. 

```xml
<add key="ideProperties:EnableImport" value="true"/>
```

If the setting isn't defined, the default value will be: `true`.

#### Export on save ####

You can enable import at startup, this will save rules that don't exist in the database, it will compare the guid `key` property. It won't update rules, except, if the property `IsDeleted` of the rule is true. 

```xml
<add key="hideProperties:ImportAtStartup" value="false"/>
```

If the setting isn't defined, the default value will be: `false`. If the setting `Enable import` is false, this rule does nothing.
