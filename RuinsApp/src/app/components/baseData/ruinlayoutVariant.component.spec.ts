import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {RuinlayoutVariantComponent} from './ruinlayoutVariant.component';

describe('RuinlayoutVariantComponent', () => {
	let component: RuinlayoutVariantComponent;
	let fixture: ComponentFixture<RuinlayoutVariantComponent>;

	beforeEach(async(() => {
		TestBed.configureTestingModule({
			declarations: [RuinlayoutVariantComponent]
		})
			.compileComponents();
	}));

	beforeEach(() => {
		fixture = TestBed.createComponent(RuinlayoutVariantComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
