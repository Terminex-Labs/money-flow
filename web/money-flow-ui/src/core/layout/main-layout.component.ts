import { Component, ViewEncapsulation } from "@angular/core";
import { RouterOutlet } from '@angular/router';

@Component({
    selector: 'app-main-layout',
    templateUrl: 'main-layout.component.html',
    styleUrls: ['main-layout.component.scss'],
    standalone: true,
    // encapsulation: ViewEncapsulation.None,
    imports: [RouterOutlet]
})

export class MainLayoutComponent {

}