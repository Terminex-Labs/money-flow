import { Component, Input, forwardRef, signal, computed, viewChild, HostListener, Optional, Self, booleanAttribute, Output, EventEmitter } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NgControl } from '@angular/forms';
import { CdkOverlayOrigin, CdkConnectedOverlay, ConnectedPosition } from '@angular/cdk/overlay';
import { CdkListbox, CdkOption } from '@angular/cdk/listbox';

export interface ComboBoxOption<T = any> {
    value: T;
    label: string;
}

@Component({
    selector: 'combobox',
    standalone: true,
    imports: [CdkOverlayOrigin, CdkConnectedOverlay, CdkListbox, CdkOption],
    providers: [{
        provide: NG_VALUE_ACCESSOR,
        useExisting: forwardRef(() => ComboBoxComponent),
        multi: true
    }],
    templateUrl: 'combobox.component.html',
    styleUrl: 'combobox.component.scss'
})
export class ComboBoxComponent<T = any> implements ControlValueAccessor {
    @Input() options: ComboBoxOption<T>[] = [];
    @Input() placeholder: string = '';
    @Input({ transform: booleanAttribute }) disabled: boolean = false;
    @Input() 
    set value(val: T | null) {
        this.selectedValue.set(val);
    }
    get value(): T | null {
        return this.selectedValue();
    }
    
    @Output() valueChange = new EventEmitter<T | null>();

    trigger = viewChild.required<CdkOverlayOrigin>('trigger');
    isOpen = signal(false);
    selectedValue = signal<T | null>(null);
    triggerWidth = signal<number>(0);

    positions: ConnectedPosition[] = [
        { originX: 'start', originY: 'bottom', overlayX: 'start', overlayY: 'top' },
        { originX: 'start', originY: 'top', overlayX: 'start', overlayY: 'bottom' }
    ];

    hasValue = computed(() => {
        const val = this.selectedValue();
        return val !== null && val !== undefined && val !== '';
    });

    selectedLabel = computed(() => {
        const val = this.selectedValue();
        const option = this.options.find(o => o.value === val);
        return option ? option.label : String(val);
    });

    constructor(@Optional() @Self() public ngControl: NgControl) {
        if (this.ngControl) {
            this.ngControl.valueAccessor = this;
        }
    }

    private onChange: any = () => { };
    private onTouch: any = () => { };

    writeValue(value: T): void {
        this.selectedValue.set(value);
    }

    registerOnChange(fn: any): void {
        this.onChange = fn;
    }

    registerOnTouched(fn: any): void {
        this.onTouch = fn;
    }

    setDisabledState(isDisabled: boolean): void {
        this.disabled = isDisabled;
    }

    toggleDropdown() {
        if (!this.disabled) {
            const newOpenState = !this.isOpen();
            this.isOpen.set(newOpenState);
            
            if (newOpenState) {
                this.onTouch();
                setTimeout(() => {
                    const triggerElement = this.trigger().elementRef.nativeElement;
                    this.triggerWidth.set(triggerElement.offsetWidth);
                }, 0);
            }
        }
    }

    onListboxChange(event: any) {
        const value = event.value?.[0] ?? null;
        this.selectedValue.set(value);
        this.isOpen.set(false);
        this.onChange(value);
        this.valueChange.emit(value);
    }

    @HostListener('document:keydown.escape')
    onClose() {
        this.isOpen.set(false);
    }
}