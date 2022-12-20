import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FileLoadRoutingModule } from './file-load-routing.module';
import { UploadComponent } from './upload/upload.component';


@NgModule({
    declarations: [
        UploadComponent
    ],
    exports: [
        UploadComponent
    ],
    imports: [
        CommonModule,
        FileLoadRoutingModule
    ]
})
export class FileLoadModule { }
