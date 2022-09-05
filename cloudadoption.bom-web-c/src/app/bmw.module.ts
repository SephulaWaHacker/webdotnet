import { NgModule } from "@angular/core";
import { 
    BmwThemeModule, 
    ButtonModule, 
    GenericTileModule, 
    BmwAgGridThemeModule, 
    RadiobuttonModule, 
    IconButtonModule, 
    InputfieldModule, 
    ComboBoxModule,
    DialogModule, 
    InlineMessageModule, 
    ProgressCircleModule, 
    DatepickerModule, 
    DropdownModule, 
    CheckboxModule,
    ReactiveFormsModule,
  } 
  from "@bmw-ds/components";


@NgModule({
  imports: [
    BmwThemeModule,
    ButtonModule,
    GenericTileModule,
    BmwAgGridThemeModule,
    RadiobuttonModule,
    IconButtonModule,
    InputfieldModule,
    ComboBoxModule,
    DialogModule,
    InlineMessageModule,
    ProgressCircleModule,
    DatepickerModule,
    DropdownModule,
    CheckboxModule,
    ReactiveFormsModule
  ],
  exports:[
    BmwThemeModule,
    ButtonModule,
    GenericTileModule,
    BmwAgGridThemeModule,
    RadiobuttonModule,
    IconButtonModule,
    InputfieldModule,
    ComboBoxModule,
    DialogModule,
    InlineMessageModule,
    ProgressCircleModule,
    DatepickerModule,
    DropdownModule,
    CheckboxModule,
    ReactiveFormsModule
  ]
})
export class BmwModule {
}
