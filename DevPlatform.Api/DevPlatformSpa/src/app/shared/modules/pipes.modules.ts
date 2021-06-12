import { NgModule } from '@angular/core';

/* Pipes */
import { NoSanitizePipe } from '../../core/pipes/no-sanitize.pipe';
import { FormatTitlePipe } from '../../core/pipes/format-title.pipe';

@NgModule({
    imports: [
    ],
    declarations: [
        NoSanitizePipe,
        FormatTitlePipe
    ],
    exports: [
        NoSanitizePipe,
        FormatTitlePipe
    ]
})
export class PipesModule { }