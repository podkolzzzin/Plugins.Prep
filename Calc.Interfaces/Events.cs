using System.Collections.Immutable;
using MediatR;

namespace Calc.Interfaces;

public record CalculatedEvent(ICalcAction Action, ImmutableArray<OperandValue> Operands) : INotification;