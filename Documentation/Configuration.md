# Configuration #

## Contents

* [Export](#export)
* [Import](#import)

---

### Export ###

If you export rules, the rules will be saved inside the config directory of Umbraco in the file: `hideProperties.rules.js`.

Path: `~/config/hideProperties.rules.js`.

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

Path: `~/config/hideProperties.rules.js`.

```json
[
  {
    "Key": "cc334a40-5818-47ba-9112-a5dd3f8ca5da",
    "IsActive": true,
    "ContentTypeAlias": "blog",
    "Tabs": "Settings",
    "Properties": "bodyText,pageTitle",
    "UserGroups": "sensitiveData,translator",
    "IsDeleted": false
  },
  ...
]
```

You can add the following keys to your appSettings in the web.config.

#### Enable import ####

You can enable import of rules, if enabled, this will display a import button in the dashbaord. This will save rules that don't exist in the database, it will compare the guid `key` property. You can change the options to update or delete of existing rules.

```xml
<add key="ideProperties:EnableImport" value="true"/>
```

If the setting isn't defined, the default value will be: `true`.

#### Import as startup ####

You can enable import at startup, this will import the rules when the application is started.

```xml
<add key="hideProperties:ImportAtStartup" value="false"/>
```

If the setting isn't defined, the default value will be: `false`. If the setting `Enable import` is false, this rule does nothing.

#### Update rule at import ####

The rule with the same key will be overwritten with the one from the file. If `IsDeleted` is true, the rule will be updated and deleted.

```xml
<add key="hideProperties:UpdateRuleAtImport" value="false"/>
```

If the setting isn't defined, the default value will be: `false`. If the setting `Enable import` is false, this rule does nothing.

#### Delete rule at import ####

The rule with the same key will be deleted.

```xml
<add key="hideProperties:DeleteRuleAtImport" value="true"/>
```

If the setting isn't defined, the default value will be: `true`. If the setting `Enable import` is false, this rule does nothing.
