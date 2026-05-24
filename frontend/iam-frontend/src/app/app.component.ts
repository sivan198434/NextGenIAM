import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommandPaletteComponent } from './shared/components/command-palette/command-palette.component';
import { WorkflowDesignerComponent } from './features/workflow-designer/workflow-designer.component';

@Component({
  imports: [RouterModule, CommandPaletteComponent, WorkflowDesignerComponent],
  selector: 'app-root',
  template: `
    <div class="min-h-screen bg-slate-100 font-sans text-slate-900">
      <!-- Nav -->
      <nav class="bg-white border-b border-slate-200 px-6 py-4 flex items-center justify-between shadow-sm sticky top-0 z-10">
        <div class="flex items-center gap-2">
          <div class="w-8 h-8 bg-indigo-600 rounded-lg flex items-center justify-center text-white font-bold">I</div>
          <span class="text-xl font-bold tracking-tight text-slate-800">NextGen <span class="text-indigo-600">IAM</span></span>
        </div>
        <div class="hidden md:flex gap-8 text-sm font-medium text-slate-600">
          <a class="hover:text-indigo-600 transition">Dashboard</a>
          <a class="hover:text-indigo-600 transition">Identities</a>
          <a class="hover:text-indigo-600 transition text-indigo-600 border-b-2 border-indigo-600">Workflows</a>
          <a class="hover:text-indigo-600 transition">Governance</a>
          <a class="hover:text-indigo-600 transition">Audit</a>
        </div>
        <div class="flex items-center gap-4">
          <span class="text-xs bg-slate-100 px-2 py-1 rounded text-slate-500 font-mono">Press ⌘K</span>
          <div class="w-8 h-8 rounded-full bg-slate-200 border border-slate-300"></div>
        </div>
      </nav>

      <main class="max-w-7xl mx-auto px-6 py-8">
        <header class="mb-8">
          <h1 class="text-3xl font-bold text-slate-900">Workflow Designer</h1>
          <p class="text-slate-500 mt-1">Design and automate lifecycle processes with zero-code visual tools.</p>
        </header>

        <section class="bg-white p-1 rounded-xl shadow-sm border border-slate-200">
          <app-workflow-designer />
        </section>

        <section class="mt-12 grid grid-cols-1 md:grid-cols-3 gap-6">
          <div class="bg-white p-6 rounded-xl border border-slate-200 shadow-sm">
            <h3 class="font-bold text-slate-800 mb-2">Audit Integrity</h3>
            <p class="text-sm text-slate-500">All workflow changes are cryptographically chained using SHA-256 to ensure an immutable audit trail.</p>
          </div>
          <div class="bg-white p-6 rounded-xl border border-slate-200 shadow-sm">
            <h3 class="font-bold text-slate-800 mb-2">Policy-as-Code</h3>
            <p class="text-sm text-slate-500">Workflows automatically respect Segregation of Duties (SoD) policies defined in the Governance engine.</p>
          </div>
          <div class="bg-white p-6 rounded-xl border border-slate-200 shadow-sm">
            <h3 class="font-bold text-slate-800 mb-2">JIT Provisioning</h3>
            <p class="text-sm text-slate-500">Integrate Just-In-Time access requests directly into your provisioning workflows for maximum security.</p>
          </div>
        </section>
      </main>

      <app-command-palette />
    </div>
  `,
})
export class AppComponent {
  title = 'portal';
}
