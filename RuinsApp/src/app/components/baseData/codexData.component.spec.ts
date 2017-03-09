import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CodexDataComponent} from './codexData.component';

describe('CodexDataComponent', () => {
	let component: CodexDataComponent;
	let fixture: ComponentFixture<CodexDataComponent>;

	beforeEach(async(() => {
		TestBed.configureTestingModule({
			declarations: [CodexDataComponent]
		})
			.compileComponents();
	}));

	beforeEach(() => {
		fixture = TestBed.createComponent(CodexDataComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create', () => {
		expect(component).toBeTruthy();
	});
});
