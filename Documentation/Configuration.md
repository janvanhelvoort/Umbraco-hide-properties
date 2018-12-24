# Configuration #

## Export ##
You can export rules from the database to a file, in multiple ways. You can enable auto export on save, or manually through the back office. You can add the following key to your appSettings in the web.config.

```xml
<add key="hideProperties:ExportOnSave" value="false"/>
```

If the setting isn't defined, the default value will be: false. If the setting is defined and true, the button to export manually is not visible..