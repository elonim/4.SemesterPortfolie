import random
import sys
from tkinter import Button, Label
import settings
import ctypes


class Cell:
    all = []
    cell_count = settings.CELL_COUNT
    cell_count_label_object = None

    def __init__(self, x, y, is_mine=False):
        self.is_mine = is_mine
        self.x = x
        self.y = y
        self.is_opened = False
        self.is_mineCandidate = False
        self.cell_btn_object = None

        # Append the pbject to the Cell.all list
        Cell.all.append(self)

    def Create_btn_object(self, location):
        btn = Button(
            location,
            width=12,
            height=4,
        )
        btn.bind('<Button-1>', self.left_click_actions)  # Left click
        btn.bind('<Button-3>', self.right_click_actions)  # Right click
        self.cell_btn_object = btn

    @staticmethod
    def create_cell_count_lable(location):
        lbl = Label(
            location,
            text=f"Cells Left: {Cell.cell_count}",
            bg='black',
            fg='white',
            font=("", 30)
        )
        Cell.cell_count_label_object = lbl

    @staticmethod
    def randomize_mines():
        picked_cells = random.sample(
            Cell.all, settings.MINES_COUNT
        )
        for picked_cell in picked_cells:
            picked_cell.is_mine = True

    def __repr__(self):
        return f"Cell({self.x}, {self.y})"

    @property
    def surrounded_cells(self):
        cells = [
            self.get_cell_by_axis(self.x - 1, self.y - 1),
            self.get_cell_by_axis(self.x - 1, self.y),
            self.get_cell_by_axis(self.x - 1, self.y + 1),
            self.get_cell_by_axis(self.x, self.y - 1),
            self.get_cell_by_axis(self.x + 1, self.y - 1),
            self.get_cell_by_axis(self.x + 1, self.y),
            self.get_cell_by_axis(self.x + 1, self.y + 1),
            self.get_cell_by_axis(self.x, self.y + 1)
        ]
        return [cell for cell in cells if cell is not None]

    def get_cell_by_axis(self, x, y):
        # return a cell object based on the value of x,y
        for cell in Cell.all:
            if cell.x == x and cell.y == y:
                return cell

    @property
    def surrounded_cells_mines_length(self):
        counter = 0
        for cell in self.surrounded_cells:
            if cell.is_mine:
                counter += 1
        return counter

    def left_click_actions(self, event):
        if self.is_mine:
            self.show_mine()
        else:
            if self.surrounded_cells_mines_length == 0:
                for cell_obj in self.surrounded_cells:
                    cell_obj.show_cell()
            self.show_cell()
            if Cell.cell_count == settings.MINES_COUNT:
                ctypes.windll.user32.MessageBoxW(
                    0,
                    'Victory!',
                    'You won the game!',
                    0
                )

        self.cell_btn_object.unbind('<Button-1>')
        self.cell_btn_object.unbind('<Button-3>')

    def show_cell(self):
        if not self.is_opened:
            Cell.cell_count -= 1
            self.cell_btn_object.configure(
                text=self.surrounded_cells_mines_length)
            # Replace text of cell count
            if Cell.cell_count_label_object:
                Cell.cell_count_label_object.configure(
                    text=f"Cells Left: {Cell.cell_count}"
                )
                self.cell_btn_object.configure(bg='SystemButtonFace')
        self.is_opened = True

    def show_mine(self):
        self.cell_btn_object.configure(bg='red')
        ctypes.windll.user32.MessageBoxW(
            0,
            'You Clicked on a mine',
            'Game Over',
            0
        )
        sys.exit()

    def right_click_actions(self, event):
        if not self.is_mineCandidate:
            self.cell_btn_object.configure(
                bg='orange'
            )
            self.is_mineCandidate = True
        else:
            self.cell_btn_object.configure(
                bg='SystemButtonFace'
            )
            self.is_mineCandidate = False
