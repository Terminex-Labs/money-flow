import { Component, computed, inject, signal, ViewEncapsulation } from "@angular/core";
import { Router, RouterOutlet } from '@angular/router';

@Component({
    selector: 'app-main-layout',
    templateUrl: 'main-layout.component.html',
    styleUrls: ['main-layout.component.scss'],
    standalone: true,
    // encapsulation: ViewEncapsulation.None,
    imports: [RouterOutlet]
})

export class MainLayoutComponent {
    private router = inject(Router);

    private titleRU = new Map<ViewPage, string>
    ([
        ['dashboard', "Главная"],
        ['transaction', "Транзакции"],
        ['budget', "Бюджет"],
        ['debt', "Долги"],
        ['account', "Счета"]
    ]);

    isSidebarExpanded = signal<boolean>(true);
    currentPage = signal<ViewPage>('dashboard');
    title = computed(() => this.titleRU.get(this.currentPage()));

    navigate(selectedPage: ViewPage) {
        this.currentPage.set(selectedPage);
        this.router.navigate([`/${this.currentPage()}`]);
    }
}