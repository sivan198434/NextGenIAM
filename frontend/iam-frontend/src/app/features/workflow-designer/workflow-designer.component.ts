import { Component, signal } from '@angular/core';

interface Node {
  id: string;
  x: number;
  y: number;
  label: string;
}

@Component({
  selector: 'app-workflow-designer',
  standalone: true,
  template: `
    <div class="w-full h-[600px] bg-slate-50 relative overflow-hidden border border-slate-200 rounded-lg shadow-inner"
         (mousemove)="onMouseMove($event)"
         (mouseup)="onMouseUp()">
      <svg class="w-full h-full">
        <!-- Grid pattern -->
        <defs>
          <pattern id="grid" width="20" height="20" patternUnits="userSpaceOnUse">
            <path d="M 20 0 L 0 0 0 20" fill="none" stroke="#e2e8f0" stroke-width="0.5"/>
          </pattern>
        </defs>
        <rect width="100%" height="100%" fill="url(#grid)" />

        <!-- Connections -->
        @for (connection of connections(); track $index) {
          <line [attr.x1]="getNode(connection.from).x + 60"
                [attr.y1]="getNode(connection.from).y + 20"
                [attr.x2]="getNode(connection.to).x + 60"
                [attr.y2]="getNode(connection.to).y + 20"
                stroke="#94a3b8" stroke-width="2" />
        }

        <!-- Nodes -->
        @for (node of nodes(); track node.id) {
          <g [attr.transform]="'translate(' + node.x + ',' + node.y + ')'"
             (mousedown)="onMouseDown(node, $event)"
             class="cursor-move">
            <rect width="120" height="40" rx="8" fill="white" stroke="#64748b" stroke-width="2" />
            <text x="60" y="25" text-anchor="middle" class="text-xs font-semibold select-none">{{node.label}}</text>
          </g>
        }
      </svg>

      <div class="absolute top-4 left-4 flex gap-2">
        <button (click)="addNode()" class="bg-indigo-600 text-white px-3 py-1 rounded text-sm shadow hover:bg-indigo-700 transition">Add Step</button>
      </div>
    </div>
  `,
  styles: [`
    :host { display: block; }
  `]
})
export class WorkflowDesignerComponent {
  nodes = signal<Node[]>([
    { id: '1', x: 50, y: 50, label: 'Start' },
    { id: '2', x: 250, y: 50, label: 'Approve' }
  ]);

  connections = signal<{from: string, to: string}[]>([
    { from: '1', to: '2' }
  ]);

  selectedNode: Node | null = null;
  dragOffset = { x: 0, y: 0 };

  getNode(id: string) {
    return this.nodes().find(n => n.id === id)!;
  }

  addNode() {
    const id = (this.nodes().length + 1).toString();
    this.nodes.update(ns => [...ns, { id, x: 100, y: 200, label: 'New Step ' + id }]);
  }

  onMouseDown(node: Node, event: MouseEvent) {
    this.selectedNode = node;
    this.dragOffset = {
      x: event.clientX - node.x,
      y: event.clientY - node.y
    };
  }

  onMouseMove(event: MouseEvent) {
    if (this.selectedNode) {
      const x = event.clientX - this.dragOffset.x;
      const y = event.clientY - this.dragOffset.y;

      this.nodes.update(ns => ns.map(n =>
        n.id === this.selectedNode?.id ? { ...n, x, y } : n
      ));
    }
  }

  onMouseUp() {
    this.selectedNode = null;
  }
}
